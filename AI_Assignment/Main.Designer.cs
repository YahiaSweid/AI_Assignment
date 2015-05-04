namespace AI
{
    partial class Main
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtResult = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnLine = new System.Windows.Forms.ToolStripButton();
            this.btnCircle = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnColor = new System.Windows.Forms.ToolStripButton();
            this.btnClean = new System.Windows.Forms.ToolStripButton();
            this.btnDebug = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnThreadControl = new System.Windows.Forms.ToolStripButton();
            this.btnAlgorithms = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnDFS = new System.Windows.Forms.ToolStripMenuItem();
            this.btnBFS = new System.Windows.Forms.ToolStripMenuItem();
            this.sheet = new System.Windows.Forms.Panel();
            this.btnHillClimbing = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.txtStatus,
            this.txtResult});
            this.statusStrip1.Location = new System.Drawing.Point(0, 442);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(820, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel1.Text = "Status:";
            // 
            // txtStatus
            // 
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(42, 17);
            this.txtStatus.Text = "ready !";
            // 
            // txtResult
            // 
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLine,
            this.btnCircle,
            this.toolStripSeparator1,
            this.btnColor,
            this.btnClean,
            this.btnDebug,
            this.toolStripSeparator2,
            this.btnThreadControl,
            this.btnAlgorithms});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.ShowItemToolTips = false;
            this.toolStrip1.Size = new System.Drawing.Size(820, 25);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnLine
            // 
            this.btnLine.CheckOnClick = true;
            this.btnLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLine.Image = global::AI.Properties.Resources.line;
            this.btnLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(23, 22);
            this.btnLine.Text = "toolStripButton1";
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // btnCircle
            // 
            this.btnCircle.CheckOnClick = true;
            this.btnCircle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCircle.Image = global::AI.Properties.Resources.circle;
            this.btnCircle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCircle.Name = "btnCircle";
            this.btnCircle.Size = new System.Drawing.Size(23, 22);
            this.btnCircle.Text = "toolStripButton2";
            this.btnCircle.Click += new System.EventHandler(this.btnCircle_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnColor
            // 
            this.btnColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnColor.Image = global::AI.Properties.Resources.Paint;
            this.btnColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(23, 22);
            this.btnColor.Text = "toolStripButton1";
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnClean
            // 
            this.btnClean.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClean.Image = global::AI.Properties.Resources.clean;
            this.btnClean.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(23, 22);
            this.btnClean.Text = "toolStripButton1";
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // btnDebug
            // 
            this.btnDebug.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDebug.Image = global::AI.Properties.Resources.debug;
            this.btnDebug.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(23, 22);
            this.btnDebug.Text = "toolStripButton1";
            this.btnDebug.Click += new System.EventHandler(this.btnDebug_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnThreadControl
            // 
            this.btnThreadControl.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnThreadControl.Image = global::AI.Properties.Resources.Play;
            this.btnThreadControl.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnThreadControl.Name = "btnThreadControl";
            this.btnThreadControl.Size = new System.Drawing.Size(23, 22);
            this.btnThreadControl.Text = "toolStripButton1";
            this.btnThreadControl.Click += new System.EventHandler(this.btnThreadControl_Click);
            // 
            // btnAlgorithms
            // 
            this.btnAlgorithms.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAlgorithms.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDFS,
            this.btnBFS,
            this.btnHillClimbing});
            this.btnAlgorithms.Image = global::AI.Properties.Resources.search;
            this.btnAlgorithms.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAlgorithms.Name = "btnAlgorithms";
            this.btnAlgorithms.Size = new System.Drawing.Size(29, 22);
            this.btnAlgorithms.Text = "toolStripDropDownButton1";
            // 
            // btnDFS
            // 
            this.btnDFS.CheckOnClick = true;
            this.btnDFS.Name = "btnDFS";
            this.btnDFS.Size = new System.Drawing.Size(178, 22);
            this.btnDFS.Text = "Depth-first Search";
            this.btnDFS.Click += new System.EventHandler(this.btnDFS_Click);
            // 
            // btnBFS
            // 
            this.btnBFS.CheckOnClick = true;
            this.btnBFS.Name = "btnBFS";
            this.btnBFS.Size = new System.Drawing.Size(178, 22);
            this.btnBFS.Text = "Breadth-first Search";
            this.btnBFS.Click += new System.EventHandler(this.btnBFS_Click);
            // 
            // sheet
            // 
            this.sheet.BackColor = System.Drawing.Color.LightSlateGray;
            this.sheet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sheet.Cursor = System.Windows.Forms.Cursors.Cross;
            this.sheet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sheet.Location = new System.Drawing.Point(0, 25);
            this.sheet.Name = "sheet";
            this.sheet.Size = new System.Drawing.Size(820, 417);
            this.sheet.TabIndex = 8;
            this.sheet.MouseClick += new System.Windows.Forms.MouseEventHandler(this.sheet_MouseClick);
            // 
            // btnHillClimbing
            // 
            this.btnHillClimbing.CheckOnClick = true;
            this.btnHillClimbing.Name = "btnHillClimbing";
            this.btnHillClimbing.Size = new System.Drawing.Size(178, 22);
            this.btnHillClimbing.Text = "Hill Climbing";
            this.btnHillClimbing.Click += new System.EventHandler(this.btnHillClimbing_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 464);
            this.Controls.Add(this.sheet);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AI Assignment";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;    
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnLine;
        private System.Windows.Forms.ToolStripButton btnCircle;
        private System.Windows.Forms.ToolStripButton btnDebug;
        private System.Windows.Forms.ToolStripButton btnColor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Panel sheet;
        private System.Windows.Forms.ToolStripDropDownButton btnAlgorithms;
        private System.Windows.Forms.ToolStripMenuItem btnDFS;
        private System.Windows.Forms.ToolStripButton btnClean;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Windows.Forms.ToolStripStatusLabel txtResult;
        private System.Windows.Forms.ToolStripMenuItem btnBFS;
        private System.Windows.Forms.ToolStripMenuItem btnHillClimbing;
        public System.Windows.Forms.ToolStripStatusLabel txtStatus;
        public System.Windows.Forms.ToolStripButton btnThreadControl;

    }
}

