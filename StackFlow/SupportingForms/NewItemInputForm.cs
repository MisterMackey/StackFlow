using StackFlow.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StackFlow.SupportingForms
{
    public partial class NewItemInputForm : Form
    {
        public NewItemInputForm()
        {
            InitializeComponent();
            TextBoxName.Focus();
            ListBoxPriority.Items.AddRange(FillPrioItems());
            KeyUp += CloseOnEnter;
            ButtonOk.Click += CloseOnOk;
            KeyPreview = true;
        }
        public NewItemInputForm(WorkStackItemPriority defaultPriority, string name, string descr) : this()
        {
            ListBoxPriority.SelectedItem = defaultPriority;
            TextBoxName.Text = name;
            RichTextBoxDescription.Text = descr;
        }



        public string NameResult { get => TextBoxName.Text; }
        public string DescriptionResult { get => RichTextBoxDescription.Text; }
        public WorkStackItemPriority PriorityResult { get => (WorkStackItemPriority)ListBoxPriority.SelectedItem; }


        private void CloseOnEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && (e.Modifiers & Keys.Modifiers) == Keys.None)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void CloseOnOk(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private object[] FillPrioItems()
        {
            var values = Enum.GetValues(typeof(Models.WorkStackItemPriority));
            object[] ret = new object[values.Length];
            values.CopyTo(ret, 0);
            return ret;
        }
    }
}
