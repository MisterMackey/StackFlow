namespace StackFlow.SupportingForms
{
    partial class PlotForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TabMainPage = new System.Windows.Forms.TabControl();
            this.TimeStats = new System.Windows.Forms.TabPage();
            this.CompletedItems = new System.Windows.Forms.TabPage();
            this.GroupBoxFilterControls = new System.Windows.Forms.GroupBox();
            this.DateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.DateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.LabelFrom = new System.Windows.Forms.Label();
            this.LabelTo = new System.Windows.Forms.Label();
            this.TextBoxTextFilter = new System.Windows.Forms.TextBox();
            this.TabMainPage.SuspendLayout();
            this.GroupBoxFilterControls.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabMainPage
            // 
            this.TabMainPage.Controls.Add(this.TimeStats);
            this.TabMainPage.Controls.Add(this.CompletedItems);
            this.TabMainPage.Location = new System.Drawing.Point(3, 3);
            this.TabMainPage.Name = "TabMainPage";
            this.TabMainPage.SelectedIndex = 0;
            this.TabMainPage.Size = new System.Drawing.Size(1362, 1002);
            this.TabMainPage.TabIndex = 0;
            // 
            // TimeStats
            // 
            this.TimeStats.Location = new System.Drawing.Point(4, 24);
            this.TimeStats.Name = "TimeStats";
            this.TimeStats.Padding = new System.Windows.Forms.Padding(3);
            this.TimeStats.Size = new System.Drawing.Size(1354, 974);
            this.TimeStats.TabIndex = 0;
            this.TimeStats.Text = "Time Stats";
            this.TimeStats.UseVisualStyleBackColor = true;
            // 
            // CompletedItems
            // 
            this.CompletedItems.Location = new System.Drawing.Point(4, 24);
            this.CompletedItems.Name = "CompletedItems";
            this.CompletedItems.Padding = new System.Windows.Forms.Padding(3);
            this.CompletedItems.Size = new System.Drawing.Size(1663, 974);
            this.CompletedItems.TabIndex = 1;
            this.CompletedItems.Text = "Completed Items";
            this.CompletedItems.UseVisualStyleBackColor = true;
            // 
            // GroupBoxFilterControls
            // 
            this.GroupBoxFilterControls.Controls.Add(this.TextBoxTextFilter);
            this.GroupBoxFilterControls.Controls.Add(this.LabelTo);
            this.GroupBoxFilterControls.Controls.Add(this.LabelFrom);
            this.GroupBoxFilterControls.Controls.Add(this.DateTimePickerTo);
            this.GroupBoxFilterControls.Controls.Add(this.DateTimePickerFrom);
            this.GroupBoxFilterControls.Location = new System.Drawing.Point(1388, 27);
            this.GroupBoxFilterControls.Name = "GroupBoxFilterControls";
            this.GroupBoxFilterControls.Size = new System.Drawing.Size(274, 974);
            this.GroupBoxFilterControls.TabIndex = 1;
            this.GroupBoxFilterControls.TabStop = false;
            this.GroupBoxFilterControls.Text = "Filtering Controls";
            // 
            // DateTimePickerFrom
            // 
            this.DateTimePickerFrom.Location = new System.Drawing.Point(68, 45);
            this.DateTimePickerFrom.Name = "DateTimePickerFrom";
            this.DateTimePickerFrom.Size = new System.Drawing.Size(200, 23);
            this.DateTimePickerFrom.TabIndex = 0;
            // 
            // DateTimePickerTo
            // 
            this.DateTimePickerTo.Location = new System.Drawing.Point(68, 84);
            this.DateTimePickerTo.Name = "DateTimePickerTo";
            this.DateTimePickerTo.Size = new System.Drawing.Size(200, 23);
            this.DateTimePickerTo.TabIndex = 1;
            // 
            // LabelFrom
            // 
            this.LabelFrom.AutoSize = true;
            this.LabelFrom.Location = new System.Drawing.Point(24, 45);
            this.LabelFrom.Name = "LabelFrom";
            this.LabelFrom.Size = new System.Drawing.Size(35, 15);
            this.LabelFrom.TabIndex = 2;
            this.LabelFrom.Text = "From";
            // 
            // LabelTo
            // 
            this.LabelTo.AutoSize = true;
            this.LabelTo.Location = new System.Drawing.Point(24, 84);
            this.LabelTo.Name = "LabelTo";
            this.LabelTo.Size = new System.Drawing.Size(19, 15);
            this.LabelTo.TabIndex = 3;
            this.LabelTo.Text = "To";
            // 
            // TextBoxTextFilter
            // 
            this.TextBoxTextFilter.Location = new System.Drawing.Point(6, 165);
            this.TextBoxTextFilter.Name = "TextBoxTextFilter";
            this.TextBoxTextFilter.Size = new System.Drawing.Size(262, 23);
            this.TextBoxTextFilter.TabIndex = 4;
            // 
            // PlotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1674, 1006);
            this.Controls.Add(this.GroupBoxFilterControls);
            this.Controls.Add(this.TabMainPage);
            this.Name = "PlotForm";
            this.Text = "PlotForm";
            this.TabMainPage.ResumeLayout(false);
            this.GroupBoxFilterControls.ResumeLayout(false);
            this.GroupBoxFilterControls.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabMainPage;
        private System.Windows.Forms.TabPage TimeStats;
        private System.Windows.Forms.TabPage CompletedItems;
        private System.Windows.Forms.GroupBox GroupBoxFilterControls;
        private System.Windows.Forms.Label LabelTo;
        private System.Windows.Forms.Label LabelFrom;
        private System.Windows.Forms.DateTimePicker DateTimePickerTo;
        private System.Windows.Forms.DateTimePicker DateTimePickerFrom;
        private System.Windows.Forms.TextBox TextBoxTextFilter;
    }
}