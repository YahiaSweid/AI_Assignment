using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI {
    public partial class Form1 : Form {
        private Graphics g;

        struct Node {
            public int id;
            public Point location;

            public Node (Point location, int id) {
                this.id = id;
                this.location = location;
            }
        }

        private List<Node> nodes;

        enum Tool { NONE, LINE, CIRCLE };
        Tool currentTool = Tool.NONE;

        private Point firstPoint, secondPoint;

        private int nodesCounter = 0;
        private int edgesCounter = 0;


        public Form1 () {
            InitializeComponent();
            g = this.CreateGraphics();

            nodes = new List<Node>();

            firstPoint = new Point(0, 0);
            secondPoint = new Point(0, 0);

        }

        private void Form1_MouseClick (object sender, MouseEventArgs e) {
            if (currentTool == Tool.CIRCLE) {
                g.DrawEllipse(new Pen(Color.Black), e.Location.X, e.Location.Y, 40, 40);
                g.DrawString(nodesCounter.ToString(), new Font("Times New Roman", 12), new SolidBrush(Color.Black), new Point(e.Location.X + 12, e.Location.Y + 12));
                nodes.Add(new Node(e.Location, nodesCounter));
                nodesCounter++;
            } else if (currentTool == Tool.LINE) {
                if (firstPoint.X == 0 && firstPoint.Y == 0)
                    firstPoint = new Point(e.Location.X, e.Location.Y);
                else {
                    secondPoint = new Point(e.Location.X, e.Location.Y);
                    g.DrawLine(new Pen(Color.Black), firstPoint, secondPoint);
                    firstPoint = new Point(0, 0);
                    secondPoint = new Point(0, 0);
                    edgesCounter++;
                }
            }
        }

        private void btnLine_Click (object sender, EventArgs e) {
            currentTool = Tool.LINE;
        }

        private void btnCircle_Click (object sender, EventArgs e) {
            currentTool = Tool.CIRCLE;
        }

        private void btnColor_Click (object sender, EventArgs e) {
            for (int i = 0; i < nodes.Count; i++) {
                g.FillEllipse(new SolidBrush(Color.Red), nodes[i].location.X, nodes[i].location.Y, 40, 40);
                g.DrawString(i.ToString(), new Font("Times New Roman", 12), new SolidBrush(Color.Black), new Point(nodes[i].location.X + 12, nodes[i].location.Y + 12));
            }
        }


    }
}
