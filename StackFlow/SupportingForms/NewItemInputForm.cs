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
            KeyPress += CloseOnEnter;
            ButtonOk.Click += CloseOnOk;
        }
        public NewItemInputForm(WorkStackItemPriority defaultPriority) : this()
        {
            ListBoxPriority.SelectedItem = defaultPriority;
        }



        public string NameResult { get => TextBoxName.Text; }
        public string DescriptionResult { get => RichTextBoxDescription.Text; }
        public WorkStackItemPriority PriorityResult { get => (WorkStackItemPriority)ListBoxPriority.SelectedItem; }


        private void CloseOnEnter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else if (e.KeyChar == (char)Keys.Escape)
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
