using StackFlow.Controllers;
using StackFlow.EventArgClasses;
using StackFlow.Models;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StackFlow
{
    public partial class Form1 : Form, IStackFlowForm
    {
        private readonly IController _controller;
        private readonly int PutThisFormToForeGround;
        private const int ItemsVisibleInActiveStack = 10;
        internal StackFlowSession ActiveSession { get; set; }
        #region Events
        public event EventHandler<WorkInterruptionEventArgs> UserClicksInterrupt;
        public event EventHandler<ActiveStackModificationEventArgs> UserModifiesActiveStack;
        public event EventHandler<FloatingStackModificationEventArgs> UserModifiesFloatingStack;
        public event EventHandler<SessionSaveOrLoadEventArgs> UserSavesSession;
        public event EventHandler<SessionSaveOrLoadEventArgs> UserLoadsSession;
        public event EventHandler<HotKeyRegisterEventArgs> UserRequestsNewHotkey;
        public event EventHandler<HotKeyPressEventArgs> UserPressedHotkey;
        #endregion

        #region public
        public StackFlowSession GetActiveSession()
        {
            return ActiveSession;
        }

        public void SetActiveSession(StackFlowSession Session)
        {
            //probably trigger save event here?
            ActiveSession = Session;
        }
        public void UpdateSessionFull()
        {
            UpdateActiveStackControl();
        }
        public GroupBox ActiveStack { get => this.GroupBoxActiveStack; }
        #endregion
        public Form1(IController controller)
        {
            InitializeComponent();
            _controller = controller;
            _controller.Initialize(this);
            BindEventHandlers();
            // Modifier keys codes: Alt = 1, Ctrl = 2, Shift = 4, Win = 8
            // Compute the addition of each combination of the keys you want to be pressed
            // ALT+CTRL = 1 + 2 = 3 , CTRL+SHIFT = 2 + 4 = 6...
            PutThisFormToForeGround = 453132;
            //following register ctrl shift f as hotkey to maximize window
            UserRequestsNewHotkey?.Invoke(this, new HotKeyRegisterEventArgs()
            {
                HotKeyId = PutThisFormToForeGround
            ,
                KeyToRegister = Keys.F
            ,
                WindowHandleToRegisterHotKeyTo = this.Handle
            ,
                Modifier = 6
            ,
                Action = HotKeyableActions.BringToForeground
            });
            SetActiveSession(new StackFlowSession() { Name = "DefaultSession" });
            UpdateSessionFull();
        }

        private void BindEventHandlers()
        {
            ButtonInterrupt.Click += ButtonInterruptClick;
            ButtonModify.Click += ButtonModifyClick;
            ButtonLoad.Click += ButtonLoadClick;
            ButtonSave.Click += ButtonSaveClick;
            ButtonPush.Click += ButtonPushClick;
            ButtonPop.Click += ButtonPopClick;            
        }

        /// <summary>
        /// Method to intercept global hotkeys pressed when stackflow is not focused
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            //invoke event and let the controller deal with it
            if (m.Msg == 0x0312)
            {
                UserPressedHotkey?.Invoke(this, new HotKeyPressEventArgs()
                {
                    HandleHotKeyIsRegisteredTo = this.Handle
                    ,
                    KeyId = m.WParam.ToInt32()
                });
            }
            base.WndProc(ref m);
        }

        #region EventHandlers
        private void UpdateActiveStackControl()
        {
            var controls = GroupBoxActiveStack.Controls;
            foreach (var c in controls)
            {
                Control ct = c as Control;
                ct.Dispose();
            }
            controls.Clear();
            //get the desired contents
            var sauce = GetActiveSession().ActiveStack.ToArray();
            PictureBox[] pictureBoxes = new PictureBox[sauce.Length];
            for (int i = 0; i < sauce.Length; i++)
            {
                var box = new PictureBox();
                box.Visible = false;
                Label boxLabel = new Label();
                boxLabel.Text = sauce[i].Name;
                box.Controls.Add(boxLabel);
                Size size = new Size(GroupBoxActiveStack.Width, GroupBoxActiveStack.Height / ItemsVisibleInActiveStack);
                box.Size = size;
                pictureBoxes[i] = box;
            }
            PictureBox[] visibleBoxes;
            if (pictureBoxes.Length <= ItemsVisibleInActiveStack)
            {
                visibleBoxes = pictureBoxes;
            }
            else
            {
                visibleBoxes = new PictureBox[10];
                pictureBoxes.CopyTo(visibleBoxes, pictureBoxes.Length - ItemsVisibleInActiveStack);
            }
            for (int i = 0; i < visibleBoxes.Length; i++)
            {
                visibleBoxes[i].Visible = true;
                visibleBoxes[i].Left = 0;
                visibleBoxes[i].Top = i * (GroupBoxActiveStack.Height / ItemsVisibleInActiveStack);
            }
            GroupBoxActiveStack.Controls.AddRange(pictureBoxes);
            LabelTitleActiveStack.Text = GetActiveSession().ActiveStack.Name;
        }
        private SessionSaveOrLoadEventArgs GetUserInputSaveOrLoad(bool save)
        {
            if (save)
            {
                SessionSaveOrLoadEventArgs ret = new SessionSaveOrLoadEventArgs();
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = Environment.CurrentDirectory;
                saveFileDialog.Filter = ".dat files (default StackFlow format)|*.dat";
                saveFileDialog.Title = "Save/Load session";
                saveFileDialog.FileName = GetActiveSession().Name;
                DialogResult result = saveFileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    FileInfo info = new FileInfo(saveFileDialog.FileName);
                    ret.Folder = info.DirectoryName;
                    ret.SessionName = info.Name;
                    return ret;
                }
                else { return null; }
            }
            else
            {
                SessionSaveOrLoadEventArgs ret = new SessionSaveOrLoadEventArgs();
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Environment.CurrentDirectory;
                openFileDialog.Filter = ".dat files (default StackFlow format)|*.dat";
                openFileDialog.Title = "Save/Load session";
                openFileDialog.FileName = GetActiveSession().Name;
                DialogResult result = openFileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    FileInfo info = new FileInfo(openFileDialog.FileName);
                    ret.Folder = info.DirectoryName;
                    ret.SessionName = info.Name;
                    return ret;
                }
                else { return null; }
            }
            
        }
        private WorkStackItem GetUserInputNewItem()
        {
            WorkStackItemPriority defaultPrio = GetActiveSession().ActiveStack.Priority;
            SupportingForms.NewItemInputForm input = new SupportingForms.NewItemInputForm(defaultPrio);
            input.ShowDialog();
            if (input.DialogResult == DialogResult.OK)
            {
                return new WorkStackItem(Name: input.NameResult, Description: input.DescriptionResult, Priority: input.PriorityResult);
            }
            else return null;
        }
        private void ButtonPopClick(object sender, EventArgs e)
        {
            ActiveStackModificationEventArgs a = new ActiveStackModificationEventArgs();
            a.TypeOfChange = ActiveStackModificationTypes.ItemCompleted;
            UserModifiesActiveStack?.Invoke(sender, a);
        }

        private void ButtonPushClick(object sender, EventArgs e)
        {
            ActiveStackModificationEventArgs a = new ActiveStackModificationEventArgs();
            a.TypeOfChange = ActiveStackModificationTypes.ItemAdded;
            if ((a.NewItem = GetUserInputNewItem()) == null)
            {
                return;
            }
            UserModifiesActiveStack?.Invoke(sender, a);
        }

        private void ButtonSaveClick(object sender, EventArgs e)
        {
            SessionSaveOrLoadEventArgs a;
            if ((a = GetUserInputSaveOrLoad(true)) == null)
            {
                return;
            }
            else
            {
                UserSavesSession?.Invoke(sender, a);
            }            
        }

        private void ButtonLoadClick(object sender, EventArgs e)
        {
            SessionSaveOrLoadEventArgs a;
            if ((a = GetUserInputSaveOrLoad(false)) == null)
            {
                return;
            }
            else
            {
                UserLoadsSession?.Invoke(sender, a);
            }
        }

        private void ButtonModifyClick(object sender, EventArgs e)
        {
            ActiveStackModificationEventArgs a = new ActiveStackModificationEventArgs();
            a.TypeOfChange = ActiveStackModificationTypes.ItemModified;
            if ((a.NewItem = GetUserInputNewItem()) == null)
            {
                return;
            }
            UserModifiesActiveStack?.Invoke(sender, a);
        }

        private void ButtonInterruptClick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
