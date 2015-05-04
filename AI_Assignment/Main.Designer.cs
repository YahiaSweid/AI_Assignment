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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtResult = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnLine = new System.Windows.Forms.ToolStripButton();
            this.btnCircle = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnStartNode = new System.Windows.Forms.ToolStripButton();
            this.btnGoalNode = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnClean = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteGraph = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDebug = new System.Windows.Forms.ToolStripButton();
            this.btnThreadControl = new System.Windows.Forms.ToolStripButton();
            this.btnAlgorithms = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnDFS = new System.Windows.Forms.ToolStripMenuItem();
            this.btnBFS = new System.Windows.Forms.ToolStripMenuItem();
            this.btnHillClimbing = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtWaitingTime = new System.Windows.Forms.ToolStripTextBox();
            this.sheet = new System.Windows.Forms.Panel();
            this.btnDirectedGraph = new System.Windows.Forms.ToolStripButton();
            this.btnUndirectedGraph = new System.Windows.Forms.ToolStripButton();
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
            this.toolStripSeparator2,
            this.btnStartNode,
            this.btnGoalNode,
            this.toolStripSeparator1,
            this.btnClean,
            this.btnDeleteGraph,
            this.toolStripSeparator3,
            this.btnDebug,
            this.btnThreadControl,
            this.btnAlgorithms,
            this.toolStripSeparator4,
            this.btnDirectedGraph,
            this.btnUndirectedGraph,
            this.toolStripSeparator5,
            this.toolStripLabel1,
            this.txtWaitingTime});
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnStartNode
            // 
            this.btnStartNode.CheckOnClick = true;
            this.btnStartNode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStartNode.Image = global::AI.Properties.Resources.start;
            this.btnStartNode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStartNode.Name = "btnStartNode";
            this.btnStartNode.Size = new System.Drawing.Size(23, 22);
            this.btnStartNode.Text = "toolStripButton1";
            this.btnStartNode.Click += new System.EventHandler(this.btnStartNode_Click);
            // 
            // btnGoalNode
            // 
            this.btnGoalNode.CheckOnClick = true;
            this.btnGoalNode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGoalNode.Image = global::AI.Properties.Resources.goal;
            this.btnGoalNode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGoalNode.Name = "btnGoalNode";
            this.btnGoalNode.Size = new System.Drawing.Size(23, 22);
            this.btnGoalNode.Text = "toolStripButton1";
            this.btnGoalNode.Click += new System.EventHandler(this.btnGoalNode_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnClean
            // 
            this.btnClean.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClean.Image = global::AI.Properties.Resources.Paint;
            this.btnClean.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(23, 22);
            this.btnClean.Text = "toolStripButton1";
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // btnDeleteGraph
            // 
            this.btnDeleteGraph.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDeleteGraph.Image = global::AI.Properties.Resources.eraser;
            this.btnDeleteGraph.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteGraph.Name = "btnDeleteGraph";
            this.btnDeleteGraph.Size = new System.Drawing.Size(23, 22);
            this.btnDeleteGraph.Text = "toolStripButton1";
            this.btnDeleteGraph.Click += new System.EventHandler(this.btnDeleteGraph_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
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
            // btnHillClimbing
            // 
            this.btnHillClimbing.CheckOnClick = true;
            this.btnHillClimbing.Name = "btnHillClimbing";
            this.btnHillClimbing.Size = new System.Drawing.Size(178, 22);
            this.btnHillClimbing.Text = "Hill Climbing";
            this.btnHillClimbing.Click += new System.EventHandler(this.btnHillClimbing_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(105, 22);
            this.toolStripLabel1.Text = "Waiting Time (ms)";
            // 
            // txtWaitingTime
            // 
            this.txtWaitingTime.Name = "txtWaitingTime";
            this.txtWaitingTime.Size = new System.Drawing.Size(100, 25);
            this.txtWaitingTime.Text = "500";
            this.txtWaitingTime.TextChanged += new System.EventHandler(this.txtWaitingTime_TextChanged);
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
            // btnDirectedGraph
            // 
            this.btnDirectedGraph.Checked = true;
            this.btnDirectedGraph.CheckOnClick = true;
            this.btnDirectedGraph.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnDirectedGraph.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnDirectedGraph.Image = ((System.Drawing.Image)(resources.GetObject("btnDirectedGraph.Image")));
            this.btnDirectedGraph.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDirectedGraph.Name = "btnDirectedGraph";
            this.btnDirectedGraph.Size = new System.Drawing.Size(55, 22);
            this.btnDirectedGraph.Text = "Directed";
            this.btnDirectedGraph.Click += new System.EventHandler(this.btnDirectedGraph_Click);
            // 
            // btnUndirectedGraph
            // 
            this.btnUndirectedGraph.CheckOnClick = true;
            this.btnUndirectedGraph.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnUndirectedGraph.Image = ((System.Drawing.Image)(resources.GetObject("btnUndirectedGraph.Image")));
            this.btnUndirectedGraph.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUndirectedGraph.Name = "btnUndirectedGraph";
            this.btnUndirectedGraph.Size = new System.Drawing.Size(69, 22);
            this.btnUndirectedGraph.Text = "Undirected";
            this.btnUndirectedGraph.Click += new System.EventHandler(this.btnUndirectedGraph_Click);
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
        private System.Windows.Forms.ToolStripButton btnClean;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Panel sheet;
        private System.Windows.Forms.ToolStripDropDownButton btnAlgorithms;
        private System.Windows.Forms.ToolStripMenuItem btnDFS;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        public System.Windows.Forms.ToolStripStatusLabel txtResult;
        private System.Windows.Forms.ToolStripMenuItem btnBFS;
        private System.Windows.Forms.ToolStripMenuItem btnHillClimbing;
        public System.Windows.Forms.ToolStripStatusLabel txtStatus;
        public System.Windows.Forms.ToolStripButton btnThreadControl;
        private System.Windows.Forms.ToolStripButton btnStartNode;
        private System.Windows.Forms.ToolStripButton btnGoalNode;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton btnDeleteGraph;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox txtWaitingTime;
        private System.Windows.Forms.ToolStripButton btnDirectedGraph;
        private System.Windows.Forms.ToolStripButton btnUndirectedGraph;

    }
}

