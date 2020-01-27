﻿namespace StackFlow
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
            this.PictureBoxActiveStackBase = new System.Windows.Forms.PictureBox();
            this.ButtonInterrupt = new System.Windows.Forms.Button();
            this.ButtonModify = new System.Windows.Forms.Button();
            this.GroupBoxActiveStack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxActiveStackBase)).BeginInit();
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
            this.GroupBoxActiveStack.Controls.Add(this.PictureBoxActiveStackBase);
            this.GroupBoxActiveStack.Location = new System.Drawing.Point(590, 33);
            this.GroupBoxActiveStack.Name = "GroupBoxActiveStack";
            this.GroupBoxActiveStack.Size = new System.Drawing.Size(100, 600);
            this.GroupBoxActiveStack.TabIndex = 1;
            this.GroupBoxActiveStack.TabStop = false;
            // 
            // PictureBoxActiveStackBase
            // 
            this.PictureBoxActiveStackBase.Location = new System.Drawing.Point(0, 0);
            this.PictureBoxActiveStackBase.Name = "PictureBoxActiveStackBase";
            this.PictureBoxActiveStackBase.Size = new System.Drawing.Size(100, 50);
            this.PictureBoxActiveStackBase.TabIndex = 2;
            this.PictureBoxActiveStackBase.TabStop = false;
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 701);
            this.Controls.Add(this.ButtonModify);
            this.Controls.Add(this.ButtonInterrupt);
            this.Controls.Add(this.GroupBoxActiveStack);
            this.Controls.Add(this.ButtonPop);
            this.Controls.Add(this.ButtonPush);
            this.Controls.Add(this.ButtonLoad);
            this.Controls.Add(this.ButtonSave);
            this.Name = "Form1";
            this.Text = "StackFlow";
            this.GroupBoxActiveStack.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxActiveStackBase)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButtonSave;
        private System.Windows.Forms.Button ButtonLoad;
        private System.Windows.Forms.Button ButtonPush;
        private System.Windows.Forms.Button ButtonPop;
        private System.Windows.Forms.GroupBox GroupBoxActiveStack;
        private System.Windows.Forms.PictureBox PictureBoxActiveStackBase;
        private System.Windows.Forms.Button ButtonInterrupt;
        private System.Windows.Forms.Button ButtonModify;
    }
}

