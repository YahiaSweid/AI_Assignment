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
using System.Threading;

namespace AI {
    public partial class Form1 : Form {
        private Graphics g;
        private Graph graph;

        enum Tool { NONE, LINE, CIRCLE };
        Tool currentTool = Tool.NONE;

        private Lines lines;
        private Lines.Line line;
        
        private int nodesCounter = 0;
        
        static bool[] seen;

        public Form1 () {
            InitializeComponent();
            g = this.CreateGraphics();
            graph = new Graph();
        
            
            line = new Lines.Line(g);
            lines = new Lines(g);
        }

        //  Draw a circle (node)
        private void createNode (int radius, MouseEventArgs e) {
            g.DrawEllipse(new Pen(Color.Black), e.Location.X, e.Location.Y, radius, radius);
            g.DrawString(nodesCounter.ToString(), new Font("Times New Roman", 12), new SolidBrush(Color.Black), new Point(e.Location.X + 12, e.Location.Y + 12));
            graph.addNode(new Graph.Node(e.Location, nodesCounter, radius));
            nodesCounter++;
        }

        //  Create an edge(it's a line between two nodes, in order to get a line on screen 
        //  you have to indicate two points then call DrawLine GDI+ method).
        private void createEdge (MouseEventArgs e) {
            if (!line.gotFirstPoint) {
                if (graph.nodes.Count > 1) {
                    // Getting first point
                    line.firstPoint = new Point(e.Location.X, e.Location.Y);
                    for (int i = 0; i < graph.nodes.Count; i++) {
                        if (graph.pointInsideNode(line.firstPoint, graph.nodes[i].location, graph.nodes[i].radius)) {
                            Graph.Node n = graph.getNodeByLocation(line.firstPoint);
                            line.firstNodeIndex = i;
                            line.gotFirstPoint = true;
                            txtStatus.Text = "the Point is inside the node and we get it";
                            break;
                        } else {
                            txtStatus.Text = "First point didn't hit any node";
                        }
                    }
                } else {
                    txtStatus.Text = "Please add two nodes at least";
                }
            } else {
                // Getting second point
                line.secondPoint = new Point(e.Location.X, e.Location.Y);
                for (int i = 0; i < graph.nodes.Count; i++) {
                    if (graph.pointInsideNode(line.secondPoint, graph.nodes[i].location, graph.nodes[i].radius)) {
                        Graph.Node n = graph.getNodeByLocation(line.secondPoint);
                        if (n.radius != 0) {
                            line.secondNodeIndex = i;
                            txtStatus.Text = "the Point is inside the node and we get it";
                            
                            // Add an edge into the graph by passing 
                            // the first node index and second node index
                            graph.addEdge(line.firstNodeIndex, line.secondNodeIndex);

                            // We got two points so we can draw the line
                            // We take the first node index and the second node index for tracking purpose
                            line.draw(new Pen(Color.Black), line.firstPoint, line.secondPoint,
                                                            line.firstNodeIndex, line.secondNodeIndex);
                            lines.add(line);

                            // Clean everything
                            line = new Lines.Line(g);
                            break;
                        }
                    } else {
                        txtStatus.Text = "Second point didn't hit any node";
                    }
                }
            }
        }

        private void Form1_MouseClick (object sender, MouseEventArgs e) {
            if (currentTool == Tool.CIRCLE) {
                createNode(40, e);
            }else if (currentTool == Tool.LINE) {
                createEdge(e);
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

        private void btnNodes_Click (object sender, EventArgs e) {
            String nodes = "";
            for (int i = 0; i < graph.nodes.Count; i++) {
                nodes += " Node ("+i+"):\n"+
                         "Location: " + graph.nodes[i].location.ToString() + 
                         "\n Radius: " + graph.nodes[i].radius.ToString() + "\n";
                for (int j = 0; j < graph.nodes[i].edges.Count; j++) {
                    nodes += "Edge (" + j + "): " + graph.nodes[i].edges[j] + "\n";
                }
            }
            if(nodes != "")
                MessageBox.Show(nodes);
            else
                MessageBox.Show("You do not have nodes");
        }

        // TODO: Binary Search
        public void DFS (Graph.Node root) {
            if (root.radius != 0) {
                seen[graph.getNodeId(root)] = true;
                for (int i = 0; i < root.edges.Count; i++) {
                    Graph.Node n = graph.getNode(root.edges[i]);
                    if (!seen[root.edges[i]]) {
                        seen[root.edges[i]] = true;
                        DFS(n);
                        Thread.Sleep(1000);
                        graph.setNodeColor(g, n, Color.Gold);
                        this.Invoke((MethodInvoker)delegate() {
                            txtResult.Text += "\n" + n.value;
                        });
                    }
                }
                
            }
        }

        private void btnDFS_Click (object sender, EventArgs e) {
            if (graph.nodes.Count > 1) {
                seen = new bool[graph.nodes.Count];
                txtResult.Text = "Result: ";
                new Thread(() => DFS(graph.getNode(0))).Start();            
            }
        }



    }
}
