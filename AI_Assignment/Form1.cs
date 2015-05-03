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

        private Pen pen;

        private Lines lines;
        private Lines.Line line;
        
        private int nodesCounter = 0;
        
        static bool[] seen;

        private Thread DFSThread;

        private string correct = "[+] ";
        private string wrong = "[-] ";


        public Form1 () {
            InitializeComponent();
            g = sheet.CreateGraphics();
            
            graph = new Graph();
            
            pen = new Pen(new SolidBrush(Color.Black));
            pen.Width = 2;
            
            line = new Lines.Line(g);
            lines = new Lines(g);
        }

        private void btnCircle_Click (object sender, EventArgs e) {
            if (btnCircle.Checked) {
                currentTool = Tool.CIRCLE;
                btnLine.Checked = false;
            } else {
                currentTool = Tool.NONE;
            }
        }

        private void btnLine_Click (object sender, EventArgs e) {
            if (btnLine.Checked) {
                currentTool = Tool.LINE;
                btnCircle.Checked = false;
            } else {
                currentTool = Tool.NONE;
            }
        }


        private void sheet_MouseClick (object sender, MouseEventArgs e) {
            if (currentTool == Tool.CIRCLE) {
                createNode(40, e);
            } else if (currentTool == Tool.LINE) {
                createEdge(e);
            }
        }

        private void btnColor_Click (object sender, EventArgs e) {
            for (int i = 0; i < graph.nodes.Count; i++) {
                g.FillEllipse(new SolidBrush(Color.Red), graph.nodes[i].location.X , graph.nodes[i].location.Y , 40, 40);
                g.DrawString(i.ToString(), new Font("Times New Roman", 12), new SolidBrush(Color.Black), new Point(graph.nodes[i].location.X + 12, graph.nodes[i].location.Y + 12));
            }
        }


        private void btnClean_Click (object sender, EventArgs e) {
            for (int i = 0; i < graph.nodes.Count; i++) {
                g.FillEllipse(new SolidBrush(sheet.BackColor), graph.nodes[i].location.X, graph.nodes[i].location.Y, 40, 40);
                //g.DrawString(i.ToString(), new Font("Times New Roman", 12), new SolidBrush(Color.Black), new Point(graph.nodes[i].location.X + 12, graph.nodes[i].location.Y + 12));
            }
        }

        //  Draw a circle (node)
        private void createNode (int radius, MouseEventArgs e) {
            Point mouseClick = new Point(e.Location.X - (radius / 2), e.Location.Y - (radius / 2));
            g.DrawEllipse(pen, mouseClick.X, mouseClick.Y, radius, radius);
            g.DrawString(nodesCounter.ToString(), new Font("Times New Roman", 12), new SolidBrush(Color.Black), new Point(mouseClick.X + 12, mouseClick.Y + 12));
            graph.addNode(new Graph.Node(mouseClick, nodesCounter, radius));
            nodesCounter++;
            txtStatus.Text = correct + "New node has been created at " + mouseClick.X + "," + mouseClick.Y;
        }

        //  Create an edge(it's a line between two nodes, in order to get a line on screen 
        //  you have to indicate two points then call DrawLine GDI+ method).
        private void createEdge (MouseEventArgs e) {
            if (!line.gotFirstPoint) {
                if (graph.nodes.Count > 1) {
                    // Getting first point
                    line.firstPoint = new Point(e.Location.X - (Lines.Line.pointSize / 2), e.Location.Y - (Lines.Line.pointSize / 2));
                    for (int i = 0; i < graph.nodes.Count; i++) {
                        if (graph.pointInsideNode(line.firstPoint, graph.nodes[i].location, graph.nodes[i].radius)) {
                            Graph.Node n = graph.getNodeByLocation(line.firstPoint);
                            line.firstNodeIndex = i;
                            line.gotFirstPoint = true;
                            g.FillEllipse(new SolidBrush(Color.Blue), line.firstPoint.X, line.firstPoint.Y , Lines.Line.pointSize, Lines.Line.pointSize);
                            txtStatus.Text = correct + "New point has been placed at " + line.firstPoint.X + "," + line.firstPoint.Y;
                            break;
                        } else {
                            txtStatus.Text = wrong + "First point didn't hit any node";
                        }
                    }
                } else {
                    txtStatus.Text = wrong + "Please add two nodes at least";
                }
            } else {
                // Getting second point
                line.secondPoint = new Point(e.Location.X - (Lines.Line.pointSize / 2), e.Location.Y - (Lines.Line.pointSize / 2));
                for (int i = 0; i < graph.nodes.Count; i++) {
                    if (graph.pointInsideNode(line.secondPoint, graph.nodes[i].location, graph.nodes[i].radius)) {
                        Graph.Node n = graph.getNodeByLocation(line.secondPoint);
                        if (n.radius != 0) {
                            line.secondNodeIndex = i;
                            g.FillEllipse(new SolidBrush(Color.Blue), e.Location.X - Lines.Line.pointSize, e.Location.Y - Lines.Line.pointSize, Lines.Line.pointSize, Lines.Line.pointSize);
                            txtStatus.Text = correct + "New point has been placed at " + line.secondPoint.X + "," + line.secondPoint.Y;
                           
                            // Add an edge into the graph by passing 
                            // the first node index and second node index
                            graph.addEdge(line.firstNodeIndex, line.secondNodeIndex);

                            // We got two points so we can draw the line
                            // We take the first node index and the second node index for tracking purpose
                            line.draw(pen, line.firstPoint, line.secondPoint,
                                                            line.firstNodeIndex, line.secondNodeIndex);
                            lines.add(line);

                            txtStatus.Text = correct + "New edge has been drawn between " + line.firstNodeIndex + " and " + line.secondNodeIndex;

                            // Clean everything
                            line = new Lines.Line(g);
                            break;
                        }
                    } else {
                        txtStatus.Text = wrong + "Second point didn't hit any node";
                    }
                }
            }
        }


        private void btnDFS_Click (object sender, EventArgs e) {
            if (graph.nodes.Count > 1 && graph.edges.Count > 1) {
                seen = new bool[graph.nodes.Count];
                txtStatus.Text = "";
                DFSThread = new Thread(() => DFS(graph.getNode(0)));
                DFSThread.Start();
                btnThreadControl.Image = new Bitmap(Properties.Resources.Pause);
                btnThreadControl.Visible = true;
                txtStatus.Text = "";
            } else {
                txtStatus.Text = wrong + "Unable to find connected nodes";
            }
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
                            txtStatus.Text += n.value + " ";
                        });
                        
                    }
                }
            }
        }

        private void btnThreadControl_Click (object sender, EventArgs e) {
            if (DFSThread != null && DFSThread.IsAlive) {
                if (DFSThread.ThreadState != ThreadState.Suspended) {
                    DFSThread.Suspend();
                    btnThreadControl.Image = new Bitmap(Properties.Resources.Play);
                    txtStatus.Text = correct + "Paused !";
                } else {
                    DFSThread.Resume();
                    btnThreadControl.Image = new Bitmap(Properties.Resources.Pause);
                    txtStatus.Text = correct + "Resumed";
                }
            } else {
                btnThreadControl.Visible = false;
            }
        }


        private void btnDebug_Click (object sender, EventArgs e) {
            String nodes = "";
            for (int i = 0; i < graph.nodes.Count; i++) {
                nodes += " Node (" + i + "):\n" +
                         "Location: " + graph.nodes[i].location.ToString() +
                         "\n Radius: " + graph.nodes[i].radius.ToString() + "\n";
                for (int j = 0; j < graph.nodes[i].edges.Count; j++) {
                    nodes += "Edge (" + j + "): " + graph.nodes[i].edges[j] + "\n";
                }
            }
            if (nodes != "")
                MessageBox.Show(nodes);
            else
                txtStatus.Text = wrong + "Unable to find nodes";
        }






    }
}
