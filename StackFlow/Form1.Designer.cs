namespace StackFlow
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ButtonSave = new System.Windows.Forms.Button();
            this.ButtonLoad = new System.Windows.Forms.Button();
            this.ButtonPush = new System.Windows.Forms.Button();
            this.ButtonPop = new System.Windows.Forms.Button();
            this.GroupBoxActiveStack = new System.Windows.Forms.GroupBox();
            this.ButtonInterrupt = new System.Windows.Forms.Button();
            this.ButtonModify = new System.Windows.Forms.Button();
            this.LabelTitleActiveStack = new System.Windows.Forms.Label();
            this.ListViewSessionInactiveStacks = new System.Windows.Forms.ListView();
            this.TextBoxDescription = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ButtonSave
            // 
            this.ButtonSave.Location = new System.Drawing.Point(1152, 639);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(100, 50);
            this.ButtonSave.TabIndex = 0;
            this.ButtonSave.Text = "Save (Ctrl + S)";
            this.ButtonSave.UseVisualStyleBackColor = true;
            // 
            // ButtonLoad
            // 
            this.ButtonLoad.Location = new System.Drawing.Point(1152, 583);
            this.ButtonLoad.Name = "ButtonLoad";
            this.ButtonLoad.Size = new System.Drawing.Size(100, 50);
            this.ButtonLoad.TabIndex = 0;
            this.ButtonLoad.Text = "Load (Ctrl + L)";
            this.ButtonLoad.UseVisualStyleBackColor = true;
            // 
            // ButtonPush
            // 
            this.ButtonPush.Location = new System.Drawing.Point(590, 639);
            this.ButtonPush.Name = "ButtonPush";
            this.ButtonPush.Size = new System.Drawing.Size(50, 50);
            this.ButtonPush.TabIndex = 0;
            this.ButtonPush.Text = "Push";
            this.ButtonPush.UseVisualStyleBackColor = true;
            // 
            // ButtonPop
            // 
            this.ButtonPop.Location = new System.Drawing.Point(640, 639);
            this.ButtonPop.Name = "ButtonPop";
            this.ButtonPop.Size = new System.Drawing.Size(50, 50);
            this.ButtonPop.TabIndex = 0;
            this.ButtonPop.Text = "Pop";
            this.ButtonPop.UseVisualStyleBackColor = true;
            // 
            // GroupBoxActiveStack
            // 
            this.GroupBoxActiveStack.Location = new System.Drawing.Point(540, 33);
            this.GroupBoxActiveStack.Name = "GroupBoxActiveStack";
            this.GroupBoxActiveStack.Size = new System.Drawing.Size(200, 600);
            this.GroupBoxActiveStack.TabIndex = 1;
            this.GroupBoxActiveStack.TabStop = false;
            // 
            // ButtonInterrupt
            // 
            this.ButtonInterrupt.Location = new System.Drawing.Point(696, 639);
            this.ButtonInterrupt.Name = "ButtonInterrupt";
            this.ButtonInterrupt.Size = new System.Drawing.Size(100, 50);
            this.ButtonInterrupt.TabIndex = 0;
            this.ButtonInterrupt.Text = "Interrupt";
            this.ButtonInterrupt.UseVisualStyleBackColor = true;
            // 
            // ButtonModify
            // 
            this.ButtonModify.Location = new System.Drawing.Point(484, 639);
            this.ButtonModify.Name = "ButtonModify";
            this.ButtonModify.Size = new System.Drawing.Size(100, 50);
            this.ButtonModify.TabIndex = 0;
            this.ButtonModify.Text = "Modify";
            this.ButtonModify.UseVisualStyleBackColor = true;
            // 
            // LabelTitleActiveStack
            // 
            this.LabelTitleActiveStack.AutoSize = true;
            this.LabelTitleActiveStack.Location = new System.Drawing.Point(590, 9);
            this.LabelTitleActiveStack.Name = "LabelTitleActiveStack";
            this.LabelTitleActiveStack.Size = new System.Drawing.Size(96, 15);
            this.LabelTitleActiveStack.TabIndex = 2;
            this.LabelTitleActiveStack.Text = "Title Active Stack";
            // 
            // ListViewSessionInactiveStacks
            // 
            this.ListViewSessionInactiveStacks.HideSelection = false;
            this.ListViewSessionInactiveStacks.Location = new System.Drawing.Point(12, 33);
            this.ListViewSessionInactiveStacks.Name = "ListViewSessionInactiveStacks";
            this.ListViewSessionInactiveStacks.Size = new System.Drawing.Size(168, 553);
            this.ListViewSessionInactiveStacks.TabIndex = 3;
            this.ListViewSessionInactiveStacks.UseCompatibleStateImageBehavior = false;
            // 
            // TextBoxDescription
            // 
            this.TextBoxDescription.Location = new System.Drawing.Point(794, 33);
            this.TextBoxDescription.Multiline = true;
            this.TextBoxDescription.Name = "TextBoxDescription";
            this.TextBoxDescription.Size = new System.Drawing.Size(206, 426);
            this.TextBoxDescription.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 701);
            this.Controls.Add(this.TextBoxDescription);
            this.Controls.Add(this.ListViewSessionInactiveStacks);
            this.Controls.Add(this.LabelTitleActiveStack);
            this.Controls.Add(this.ButtonModify);
            this.Controls.Add(this.ButtonInterrupt);
            this.Controls.Add(this.GroupBoxActiveStack);
            this.Controls.Add(this.ButtonPop);
            this.Controls.Add(this.ButtonPush);
            this.Controls.Add(this.ButtonLoad);
            this.Controls.Add(this.ButtonSave);
            this.Name = "Form1";
            this.Text = "StackFlow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonSave;
        private System.Windows.Forms.Button ButtonLoad;
        private System.Windows.Forms.Button ButtonPush;
        private System.Windows.Forms.Button ButtonPop;
        private System.Windows.Forms.GroupBox GroupBoxActiveStack;
        private System.Windows.Forms.Button ButtonInterrupt;
        private System.Windows.Forms.Button ButtonModify;
        private System.Windows.Forms.Label LabelTitleActiveStack;
        private System.Windows.Forms.ListView ListViewSessionInactiveStacks;
        private System.Windows.Forms.TextBox TextBoxDescription;
    }
}

