﻿namespace AI {
    partial class DebugForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent () {
            this.txtDebug = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // txtDebug
            // 
            this.txtDebug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDebug.Location = new System.Drawing.Point(0, 0);
            this.txtDebug.Name = "txtDebug";
            this.txtDebug.Size = new System.Drawing.Size(284, 262);
            this.txtDebug.TabIndex = 0;
            this.txtDebug.Text = "";
            // 
            // DebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.txtDebug);
            this.Name = "DebugForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DebugForm";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox txtDebug;
    }
}