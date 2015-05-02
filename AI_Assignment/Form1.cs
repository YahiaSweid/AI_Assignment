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
        private Graph graph;
        private Random random = new Random();

        enum Tool { NONE, LINE, CIRCLE };
        Tool currentTool = Tool.NONE;

        private Point firstPoint, secondPoint;

        private int nodesCounter = 0;
        private int edgesCounter = 0;


        public Form1 () {
            InitializeComponent();
            g = this.CreateGraphics();
            graph = new Graph();
            
            firstPoint = new Point(0, 0);
            secondPoint = new Point(0, 0);

        }


        
        private void Form1_MouseClick (object sender, MouseEventArgs e) {
            
            if (currentTool == Tool.CIRCLE) {
                int value = random.Next(0, 9);
                g.DrawEllipse(new Pen(Color.Black), e.Location.X, e.Location.Y, 40, 40);
                g.DrawString(nodesCounter.ToString(), new Font("Times New Roman", 12), new SolidBrush(Color.Black), new Point(e.Location.X + 12, e.Location.Y + 12));
                graph.addNode(new Graph.Node(e.Location, value, 40));
                nodesCounter++;
            } else if (currentTool == Tool.LINE) {
                if (firstPoint.X == 0 && firstPoint.Y == 0) {
                    firstPoint = new Point(e.Location.X, e.Location.Y);
                    for (int i = 0; i < graph.nodes.Count; i++) {
                        if (graph.pointInsideNode(firstPoint, graph.nodes[i].location, graph.nodes[i].radius)) {
                            Graph.Node n = graph.getNodeByLocation(firstPoint);
                            if (n.radius != 0 ) {
                                // TODO: Add the First Point of the Edge
                                MessageBox.Show("the Point is inside the node and we get it");
                            }
                        }
                    }
               } else {
                    secondPoint = new Point(e.Location.X, e.Location.Y);
                    for (int i = 0; i < graph.nodes.Count; i++) {
                        if (graph.pointInsideNode(secondPoint, graph.nodes[i].location, graph.nodes[i].radius)) {
                            Graph.Node n = graph.getNodeByLocation(firstPoint);
                            if (n.radius != 0) {
                                // TODO: Add the Second Point of the Edge
                                MessageBox.Show("the Point is inside the node and we get it");
                            }
                        }
                    }
                    g.DrawLine(new Pen(Color.Black), firstPoint, secondPoint);
                    edgesCounter++;
                    
                    // Clear everything
                    firstPoint = new Point(0, 0);
                    secondPoint = new Point(0, 0);
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
            for (int i = 0; i < graph.nodes.Count; i++) {
                g.FillEllipse(new SolidBrush(Color.Red), graph.nodes[i].location.X, graph.nodes[i].location.Y, 40, 40);
                g.DrawString(i.ToString(), new Font("Times New Roman", 12), new SolidBrush(Color.Black), new Point(graph.nodes[i].location.X + 12, graph.nodes[i].location.Y + 12));
            }
        }

        private void Form1_Load (object sender, EventArgs e) {
            
           /*
            graph.addNode(new Graph.Node(6));
            graph.addNode(new Graph.Node(7));

            graph.addEdge(0, 1);
            graph.addEdge(1, 2);
            graph.addEdge(0, 2);


            MessageBox.Show("Node 1:\n first edge " + graph.getEdge(graph.getNode(1).edges[1]).first.ToString() +
                            " , second edge " + graph.getEdge(graph.getNode(1).edges[1]).second.ToString());

            */
            
        }

        private void Form1_Paint (object sender, PaintEventArgs e) {
            
        }


    }
}
