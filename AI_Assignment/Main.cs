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
    
    public enum ProgramStatus { STOPPED, PAUSED, RUNNING };

    public partial class Main : Form {
        private Graphics g;
        private Graph graph;
        private Algorithms alg;
        private Random random;

        private enum Tool { NONE, LINE, CIRCLE, STARTNODE, GOALNODE};
        private Tool currentTool = Tool.NONE;

        private enum GraphType { DIRECTED, UNDIRECTED};
        private GraphType currentGraph = GraphType.DIRECTED;

        private enum Algorithm { NONE, DFS, BFS, HillClimbing };
        private Algorithm selectedAlgorithm = Algorithm.NONE;

        public ProgramStatus programStatus = ProgramStatus.STOPPED;

        private Pen pen;

        private Lines lines;
        private Lines.Line line;

        private Graph.Node startNode;
        private Graph.Node goalNode;

        private int nodesCounter = 0;
        
        static bool[] seen;

        private Thread thread;

        private string correct = "[+] ";
        private string wrong = "[-] ";


        public Main () {
            InitializeComponent();
            g = sheet.CreateGraphics();
            
            graph = new Graph(true);
            
            pen = new Pen(new SolidBrush(Color.Black));
            pen.Width = 2;
            
            line = new Lines.Line(g);
            lines = new Lines(g);

            goalNode = new Graph.Node();
            startNode = new Graph.Node();

            alg = new Algorithms(g, graph, this);
            random = new Random();
        }

        private void btnCircle_Click (object sender, EventArgs e) {
            if (btnCircle.Checked) {
                currentTool = Tool.CIRCLE;
                btnLine.Checked = false;
                btnStartNode.Checked = false;
                btnGoalNode.Checked = false;
            } else {
                currentTool = Tool.NONE;
            }
        }

        private void btnLine_Click (object sender, EventArgs e) {
            if (btnLine.Checked) {
                currentTool = Tool.LINE;
                btnCircle.Checked = false;
                btnStartNode.Checked = false;
                btnGoalNode.Checked = false;
            } else {
                currentTool = Tool.NONE;
            }
        }


        private void btnStartNode_Click (object sender, EventArgs e) {
            if (btnStartNode.Checked) {
                currentTool = Tool.STARTNODE;
                btnGoalNode.Checked = false;
                btnLine.Checked = false;
                btnCircle.Checked = false;
            } else {
                currentTool = Tool.NONE;
            }
        }

        private void btnGoalNode_Click (object sender, EventArgs e) {
            if (btnGoalNode.Checked) {
                currentTool = Tool.GOALNODE;
                btnStartNode.Checked = false;
                btnLine.Checked = false;
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
            } else if(currentTool == Tool.STARTNODE){
                indicateStartGoalNode(false,e);
            } else if (currentTool == Tool.GOALNODE) {
                indicateStartGoalNode(true, e);
            }else{
                txtStatus.Text = wrong + "Please select one tool";
            }
        }

        private void indicateStartGoalNode (bool isGoalNode,MouseEventArgs e) {
            if (graph.nodes.Count > 1) {
                Point mouseClick = new Point(e.Location.X , e.Location.Y);
                for (int i = 0; i < graph.nodes.Count; i++) {
                    if (graph.pointInsideNode(mouseClick, graph.nodes[i].location, graph.nodes[i].radius)) {
                        Graph.Node n = graph.getNodeByLocation(mouseClick);
                        if (!isGoalNode){
                            if (startNode.radius == 0) {
                                startNode = n;
                                graph.setNodeColor(g, n, Color.LightGray);
                            } else {
                                graph.setNodeColor(g, startNode, startNode.color);
                                startNode = n;
                                graph.setNodeColor(g, n, Color.LightGray);
                            }
                            txtStatus.Text = correct + "The starting node has been indicated at " + n.value;
                        }else{
                            if (goalNode.radius == 0) {
                                goalNode = n;
                                graph.setNodeColor(g, n, Color.LightGreen);
                            } else {
                                graph.setNodeColor(g, goalNode, goalNode.color);
                                goalNode = n;
                                graph.setNodeColor(g, n, Color.LightGreen);
                            }
                            txtStatus.Text = correct + "The goal node has been indicated at " + n.value;
                        }
                        break;
                    } else {
                        txtStatus.Text = wrong + "You have to click on some node";
                    }
                }
            } else {
                txtStatus.Text = wrong + "Please add two nodes at least";
            }
        }


        private void btnClean_Click (object sender, EventArgs e) {
            for (int i = 0; i < graph.nodes.Count; i++) {
                if (graph.nodes[i].location != goalNode.location && graph.nodes[i].location != startNode.location) {
                    g.FillEllipse(new SolidBrush(sheet.BackColor), graph.nodes[i].location.X, graph.nodes[i].location.Y, 40, 40);
                    g.DrawString(graph.nodes[i].value.ToString(), new Font("Times New Roman", 12), new SolidBrush(Color.Black), new Point(graph.nodes[i].location.X + 12, graph.nodes[i].location.Y + 12));
                }
            }
        }

        //  Draw a circle (node)
        private void createNode (int radius, MouseEventArgs e) {
            Point mouseClick = new Point(e.Location.X - (radius / 2), e.Location.Y - (radius / 2));
            int cost = random.Next(100);

            g.DrawEllipse(pen, mouseClick.X, mouseClick.Y, radius, radius);
            g.DrawString(nodesCounter.ToString(), new Font("Times New Roman", 12), new SolidBrush(Color.Black), new Point(mouseClick.X + 12, mouseClick.Y + 12));
            g.DrawString(cost.ToString(), new Font("Times New Roman", 12), new SolidBrush(Color.White), new Point(mouseClick.X + 48, mouseClick.Y + 12));
            
            graph.addNode(new Graph.Node(mouseClick, nodesCounter, radius, cost, sheet.BackColor));
            
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


        private void uncheckAlgortihmsButtons () {
            btnDFS.Checked = false;
            btnBFS.Checked = false;
            btnHillClimbing.Checked = false;
        }

        private void btnDFS_Click (object sender, EventArgs e) {
            uncheckAlgortihmsButtons();
            if (graph.nodes.Count > 1 && graph.edges.Count > 1) {
                btnDFS.Checked = true;
                selectedAlgorithm = Algorithm.DFS;
            } else {
                txtStatus.Text = wrong + "Unable to find connected nodes";
            }
        }


        private void btnBFS_Click (object sender, EventArgs e) {
            uncheckAlgortihmsButtons();
            if (graph.nodes.Count > 1 && graph.edges.Count > 1) {
                btnBFS.Checked = true;
                selectedAlgorithm = Algorithm.BFS;
            } else {
                txtStatus.Text = wrong + "Unable to find connected nodes";
            }
        }


        private void btnHillClimbing_Click (object sender, EventArgs e) {
            uncheckAlgortihmsButtons();
            if (graph.nodes.Count > 1 && graph.edges.Count > 1) {
                btnHillClimbing.Checked = true;
                selectedAlgorithm = Algorithm.HillClimbing;
            } else {
                txtStatus.Text = wrong + "Unable to find connected nodes";
            }
        }


        private void btnThreadControl_Click (object sender, EventArgs e) {
            if (startNode.radius != 0 && goalNode.radius != 0) {
                if (programStatus == ProgramStatus.STOPPED) {
                    if (selectedAlgorithm == Algorithm.NONE) {
                        txtStatus.Text = "Please select one algorithm";
                        return;
                    }
                    // Start
                    seen = new bool[graph.nodes.Count];
                    if (selectedAlgorithm == Algorithm.DFS) {
                        thread = new Thread(() => alg.DFS(startNode,goalNode, seen));
                        txtStatus.Text = "DFS Starts !";
                    } else if (selectedAlgorithm == Algorithm.BFS) {
                        thread = new Thread(() => alg.BFS(startNode, goalNode, seen));
                        txtStatus.Text = "BFS Starts !";
                    } else if (selectedAlgorithm == Algorithm.HillClimbing) {
                        thread = new Thread(() => alg.HillClimbing(startNode, goalNode, seen));
                        txtStatus.Text = "Hill Climbing Starts !";
                    }
                    programStatus = ProgramStatus.RUNNING;
                    thread.IsBackground = true;
                    thread.Start();
                    btnThreadControl.Image = new Bitmap(Properties.Resources.Pause);
                    txtResult.Text = "| Result: ";
                }
                    // Pause 
                else if (programStatus == ProgramStatus.RUNNING) {
                    thread.Suspend();
                    programStatus = ProgramStatus.PAUSED;
                    btnThreadControl.Image = new Bitmap(Properties.Resources.Play);
                    txtStatus.Text = correct + "Paused !";
                }
                    // Resume
                else {
                    thread.Resume();
                    programStatus = ProgramStatus.RUNNING;
                    btnThreadControl.Image = new Bitmap(Properties.Resources.Pause);
                    txtStatus.Text = correct + "Resumed";
                }
            } else {
                txtStatus.Text = wrong + "Please indicate the starting and goal nodes before running the algorithm";
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


        private bool deleteGraph (string confirmationMessage) {
            if (graph.nodes.Count > 0) {
                if (MessageBox.Show(confirmationMessage, "Confirmation", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return false;
                
                graph.freeMemory();
                lines.freeMemory();
                nodesCounter = 0;
                txtStatus.Text = correct + "The graph has been deleted";
            }
            // Initialize everything 
            graph = new Graph(currentGraph == GraphType.DIRECTED);

            lines = new Lines(g);
            goalNode = new Graph.Node();
            startNode = new Graph.Node();
            alg = new Algorithms(g, graph, this);
            g.Clear(sheet.BackColor);
            return true;
        }

        private void Main_FormClosed (object sender, FormClosedEventArgs e) {
            graph.freeMemory();
            lines.freeMemory();
            Application.Exit();
        }

        private void cmbGraphType_KeyPress (object sender, KeyPressEventArgs e) {
            e.Handled = true;
        }

        private void cmbGraphType_TextChanged (object sender, EventArgs e) {
            
            
        }

        private void btnDeleteGraph_Click (object sender, EventArgs e) {
            deleteGraph("Do you want to delete the graph ?");
        }

        private void txtWaitingTime_TextChanged (object sender, EventArgs e) {
            try {
                alg.waitingTime = Convert.ToInt32(txtWaitingTime.Text);
            } catch (Exception ex) {
                alg.waitingTime = 500;
                txtWaitingTime.Text = Convert.ToString(500);
            }
        }

        private void btnDirectedGraph_Click (object sender, EventArgs e) {
            if (btnDirectedGraph.Checked) {
                currentGraph = GraphType.DIRECTED;
                if (deleteGraph("Changing the graph's type requires deleting the current graph\n Do you want to delete the current graph?")) {
                    btnUndirectedGraph.Checked = false;
                } else {
                    currentGraph = GraphType.UNDIRECTED;
                    btnDirectedGraph.Checked = false;
                }
            } else {
                btnDirectedGraph.Checked = true;
                currentGraph = GraphType.DIRECTED;
            }
        }

        private void btnUndirectedGraph_Click (object sender, EventArgs e) {
            if (btnUndirectedGraph.Checked) {
                currentGraph = GraphType.UNDIRECTED;
                if (deleteGraph("Changing the graph's type requires deleting the current graph\n Do you want to delete the current graph?")) {
                    btnDirectedGraph.Checked = false;
                } else {
                    currentGraph = GraphType.DIRECTED;
                    btnUndirectedGraph.Checked = false;
                }
            } else {
                btnUndirectedGraph.Checked = true;
                currentGraph = GraphType.UNDIRECTED;
            }
        }







    }
}
