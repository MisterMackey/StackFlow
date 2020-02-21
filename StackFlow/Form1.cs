using StackFlow.Controllers;
using StackFlow.EventArgClasses;
using StackFlow.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using StackFlow.Procedures;
using StackFlow.Statistics;
using StackFlow.Plotting;
using StackFlow.SupportingForms;

namespace StackFlow
{
    public partial class Form1 : Form, IStackFlowForm
    {
        private readonly IController _controller;
        private readonly int PutThisFormToForeGround;
        private const int ItemsVisibleInActiveStack = 10;
        private string TitleOfItemInActiveStackThatGotRightClicked; //used to store a string we need if user right clicks and selects an op
        private string SaveFileLocation; //gets set when loading or saving the session
        internal StackFlowSession ActiveSession { get; set; }
        #region Events
        public event EventHandler<WorkInterruptionEventArgs> UserClicksInterrupt;
        public event EventHandler<ActiveStackModificationEventArgs> UserModifiesActiveStack;
        public event EventHandler<FloatingStackModificationEventArgs> UserModifiesFloatingStack;
        public event EventHandler<SessionSaveOrLoadEventArgs> UserSavesSession;
        public event EventHandler<SessionSaveOrLoadEventArgs> UserLoadsSession;
        public event EventHandler<HotKeyRegisterEventArgs> UserRequestsNewHotkey;
        public event EventHandler<HotKeyPressEventArgs> UserPressedHotkey;
        public event EventHandler<StackSwitchEventArgs> UserRequestsStackSwitch;
        #endregion

        #region public
        public StackFlowSession GetActiveSession()
        {
            return ActiveSession;
        }

        public void SetActiveSession(StackFlowSession Session)
        {
            //probably trigger save event here?
            if (ActiveSession != null)
            {
                ActiveSession.SetInActive();
            }
            ActiveSession = Session;
            ActiveSession.SetActive();
        }
        public void UpdateSessionFull()
        {
            //might be session is empty, no update then
            if (GetActiveSession().ActiveStack == null) { return; }
            UpdateActiveStackControl();
            UpdateDescriptionControl();
            UpdateInactiveStackControl();
            Text = $"StackFlow - {GetActiveSession().Name}";
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
            //following to register ctrl s as save
            UserRequestsNewHotkey?.Invoke(this, new HotKeyRegisterEventArgs()
            {
                HotKeyId = (int)Keys.S,
                KeyToRegister = Keys.S,
                WindowHandleToRegisterHotKeyTo = Handle,
                Modifier = 6,
                Action = HotKeyableActions.Save
            });
            SetActiveSession(new StackFlowSession() { Name = "DefaultSession" });
            ListViewSessionInactiveStacks.Scrollable = true;
            ListViewSessionInactiveStacks.View = View.Details;
            //-2 = autosize
            ListViewSessionInactiveStacks.Columns.Add("Stacks", -2, HorizontalAlignment.Center);
            ListViewSessionInactiveStacks.FullRowSelect = true;    
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
            ListViewSessionInactiveStacks.ItemActivate += InactiveStackSelected;
            this.FormClosing += OnAppExit;
            TextBoxDescription.LostFocus += OnDescriptionTextBoxLostFocus;
            KeyDown += OnFormKeyDown;
            ButtonShowPlotForm.Click += OnUserWantsToSeeSomeStats;
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

        private void OnUserWantsToSeeSomeStats(object sender, EventArgs e)
        {
            PriorityDistributionOverTime data = new PriorityDistributionOverTime(new StackFlowSession[] { GetActiveSession() });
            PriorityDistributionPlotModel generator = new PriorityDistributionPlotModel();
            var model = generator.GetModel(data, DateTimeOffset.MinValue, DateTimeOffset.MaxValue);
            PlotForm form = new PlotForm(model);
            form.ShowDialog();
        }


        private void OnFormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control) //no control no hotkey
            {
                e.SuppressKeyPress = true;
                UserPressedHotkey?.Invoke(this, new HotKeyPressEventArgs()
                {
                    HandleHotKeyIsRegisteredTo = Handle,
                    KeyId = e.KeyValue,
                    Message = $"{SaveFileLocation}\\{GetActiveSession().Name}"
                }); 
            }
        }

        private void OnAppExit(object sender, FormClosingEventArgs e)
        {
            if (ActiveSession != null)
            {
                ActiveSession.SetInActive();
            }
        }
        private void InactiveStackSelected(object sender, EventArgs e)
        {
            var item = ListViewSessionInactiveStacks.SelectedItems[0];
            string nameOfSelectedStack = item.Text;
            foreach (WorkStack stack in GetActiveSession().Session)
            {
                if (stack.Name.Equals(nameOfSelectedStack))
                {
                    StackSwitchEventArgs a = new StackSwitchEventArgs();
                    a.Stack = stack;
                    UserRequestsStackSwitch?.Invoke(sender, a);
                    break;
                }
            }
        }

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
                boxLabel.Size = size;
                boxLabel.ContextMenuStrip = new ContextMenuStrip();
                boxLabel.ContextMenuStrip.Items.Add("Insert above");
                boxLabel.ContextMenuStrip.Items[0].Click += OnUserInsertsItemMiddleOfStack;
                boxLabel.ContextMenuStrip.Items.Add("Set to complete");
                boxLabel.ContextMenuStrip.Items[1].Click += OnUsersSetsItemToCompleteMiddleOfStack;
                boxLabel.Enabled = true;
                boxLabel.MouseDown += OnStackItemRightClick;
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
                //rolling my own loop cuz takewhile is no simpler and easier to read
                for (int i = 0; i < ItemsVisibleInActiveStack; i++)
                {
                    visibleBoxes[i] = pictureBoxes[i];
                }
            }
            for (int i = 0; i < visibleBoxes.Length; i++)
            {
                visibleBoxes[i].Visible = true;
                visibleBoxes[i].Left = 0;
                visibleBoxes[i].Top = i * (GroupBoxActiveStack.Height / ItemsVisibleInActiveStack);
                visibleBoxes[i].SizeMode = PictureBoxSizeMode.StretchImage;
                visibleBoxes[i].ClientSize = visibleBoxes[i].Size;
                visibleBoxes[i].Image = Properties.Resources.PauperStack;
                visibleBoxes[i].BackgroundImage = Properties.Resources.PauperStack;
            }
            GroupBoxActiveStack.Controls.AddRange(pictureBoxes);
            LabelTitleActiveStack.Text = GetActiveSession().ActiveStack.Name;
        }

        private void UpdateDescriptionControl()
        {
            if (GetActiveSession().ActiveStack.Any())
            {
                TextBoxDescription.Text = GetActiveSession().ActiveStack.Peek().Description;
            }
            else
            {
                TextBoxDescription.Text = "";
            }
            
        }

        private void UpdateInactiveStackControl()
        {
            ListViewSessionInactiveStacks.Items.Clear();
            List<ListViewItem> stacks = new List<ListViewItem>();
            string activeStackName = GetActiveSession().ActiveStack.Name;
            int i = 0;
            foreach (var stack in GetActiveSession().Session)
            {
                if (stack.Name != activeStackName)
                {
                    stacks.Add(new ListViewItem(stack.Name));
                }
            }
            ListViewSessionInactiveStacks.Items.AddRange(stacks.ToArray());
        }
        private SessionSaveOrLoadEventArgs GetUserInputSaveOrLoad(bool save)
        {
            if (save)
            {
                SessionSaveOrLoadEventArgs ret = new SessionSaveOrLoadEventArgs();
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = SaveFileLocation ?? Environment.CurrentDirectory;
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
                openFileDialog.InitialDirectory = SaveFileLocation ?? Environment.CurrentDirectory;
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
            return GetUserInputNewItem("", "");
        }
        private WorkStackItem GetUserInputNewItem(string nameField, string descriptionField)
        {
            WorkStackItemPriority defaultPrio = GetActiveSession().ActiveStack == null ? WorkStackItemPriority.Whenever : GetActiveSession().ActiveStack.Priority; 
            SupportingForms.NewItemInputForm input = new SupportingForms.NewItemInputForm(defaultPrio, nameField, descriptionField);            
            input.ShowDialog();
            if (input.DialogResult == DialogResult.OK)
            {
                return new WorkStackItem(Name: input.NameResult, Description: input.DescriptionResult, Priority: input.PriorityResult);
            }
            else return null;
        }
        private void ButtonPopClick(object sender, EventArgs e)
        {
            //might be session is empty, no update then
            if (GetActiveSession().ActiveStack == null) { return; }
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
                SaveFileLocation = a.Folder;
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
                SaveFileLocation = a.Folder;
                UserLoadsSession?.Invoke(sender, a);
            }
        }

        private void ButtonModifyClick(object sender, EventArgs e)
        {
            if (GetActiveSession().ActiveStack.Any())
            {
                ActiveStackModificationEventArgs a = new ActiveStackModificationEventArgs();
                a.TypeOfChange = ActiveStackModificationTypes.ItemModified;
                var itum = GetActiveSession().ActiveStack.Peek();
                if ((a.NewItem = GetUserInputNewItem(itum.Name, itum.Description)) == null)
                {
                    return;
                }
                UserModifiesActiveStack?.Invoke(sender, a); 
            }
        }

        private void ButtonInterruptClick(object sender, EventArgs e)
        {
            WorkInterruptionEventArgs a = new WorkInterruptionEventArgs();
            var input = GetUserInputNewItem();
            if (input == null) { return; }
            a.DescriptionOfNewStack = input.Description;
            a.NameOfNewStack = input.Name;
            UserClicksInterrupt?.Invoke(sender, a);
        }
        private void OnUsersSetsItemToCompleteMiddleOfStack(object sender, EventArgs e)
        {
            ActiveStackModificationEventArgs a = new ActiveStackModificationEventArgs();
            a.TypeOfChange = ActiveStackModificationTypes.ItemRemoved;
            string itemTitle = TitleOfItemInActiveStackThatGotRightClicked;
            WorkStackItem refToItem = GetActiveSession().ActiveStack.First(x => x.Name == itemTitle);
            a.NewItem = refToItem;
            UserModifiesActiveStack?.Invoke(sender, a);
        }

        private void OnUserInsertsItemMiddleOfStack(object sender, EventArgs e)
        {
            ActiveStackModificationEventArgs a = new ActiveStackModificationEventArgs();
            a.TypeOfChange = ActiveStackModificationTypes.ItemInserted;
            string itemTitle = TitleOfItemInActiveStackThatGotRightClicked;
            WorkStackItem refToItem = GetActiveSession().ActiveStack.First(x => x.Name == itemTitle);
            a.DesiredParentIfInserting = refToItem;
            a.NewItem = GetUserInputNewItem();
            UserModifiesActiveStack?.Invoke(sender, a);
        }

        private void OnStackItemRightClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Label ctrl = sender as Label;
                this.TitleOfItemInActiveStackThatGotRightClicked = ctrl.Text;
                ctrl.ContextMenuStrip.Show(ctrl, Cursor.Position, ToolStripDropDownDirection.Default);                
            }
        }

        private void OnDescriptionTextBoxLostFocus(object sender, EventArgs e)
        {
            if (GetActiveSession().ActiveStack.Any())
            {
                var text = TextBoxDescription.Text;
                var activeItem = GetActiveSession().ActiveStack.Peek();
                activeItem.Description = text;
                UserModifiesActiveStack(sender, new ActiveStackModificationEventArgs()
                {
                    TypeOfChange = ActiveStackModificationTypes.ItemModified,
                    NewItem = activeItem
                }); 
            }
        }
        #endregion

    }
}
