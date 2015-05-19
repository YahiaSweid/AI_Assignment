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
        private DebugForm debugForm;

        private enum Tool { NONE, LINE, CIRCLE, STARTNODE, GOALNODE, OBSTACLE};
        private Tool currentTool = Tool.NONE;

        private enum GraphType { DIRECTED, UNDIRECTED};
        private GraphType currentGraph = GraphType.DIRECTED;

        private enum Algorithm { NONE, DFS, BFS, HillClimbing, AStar };
        private Algorithm selectedAlgorithm = Algorithm.NONE;

        public ProgramStatus programStatus = ProgramStatus.STOPPED;
        private Graph.NodeType nodeType = Graph.NodeType.CIRCLE;

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
            debugForm = new DebugForm();
        }

        private void btnCircle_Click (object sender, EventArgs e) {
            if (btnCircle.Checked) {
                currentTool = Tool.CIRCLE;
                btnLine.Checked = false;
                btnStartNode.Checked = false;
                btnGoalNode.Checked = false;
                btnObstacle.Checked = false;
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
                btnObstacle.Checked = false;
            } else {
                currentTool = Tool.NONE;
            }
        }


        private void btnStartNode_Click (object sender, EventArgs e) {
            if (btnStartNode.Checked) {
                currentTool = Tool.STARTNODE;
                btnGoalNode.Checked = false;
                btnObstacle.Checked = false;
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
                btnObstacle.Checked = false;
                btnLine.Checked = false;
                btnCircle.Checked = false;
            } else {
                currentTool = Tool.NONE;
            }
        }


        private void btnObstacle_Click (object sender, EventArgs e) {
            if (btnObstacle.Checked) {
                currentTool = Tool.OBSTACLE;
                btnStartNode.Checked = false;
                btnGoalNode.Checked = false;
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
                setStartGoalNode(false,e);
            } else if (currentTool == Tool.GOALNODE) {
                setStartGoalNode(true, e);
            } else if (currentTool == Tool.OBSTACLE) {
                setObstacle(e);
            } else {
                txtStatus.Text = wrong + "Please select one tool";
            }
        }

        private void setObstacle (MouseEventArgs e) {
            if (graph.nodes.Count > 1) {
                Point mouseClick = new Point(e.Location.X, e.Location.Y);
                for (int i = 0; i < graph.nodes.Count; i++) {
                    if (graph.pointInsideNode(graph.nodes[i].type, mouseClick, graph.nodes[i].location)) {
                        Graph.Node n = graph.getNodeByLocation(graph.nodes[i].type, mouseClick);
                        if (n.location != startNode.location && n.location != goalNode.location) {
                            graph.setNodeColor(nodeType, g, n, Graph.obstacleColor);
                            n.obstacle = true;
                            n.cost = 9999;
                            graph.nodes[i] = n;
                            txtStatus.Text = correct + "New obstacle has been indicated at " + n.value;
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

        private void setStartGoalNode (bool isGoalNode,MouseEventArgs e) {
            if (graph.nodes.Count > 1) {
                Point mouseClick = new Point(e.Location.X , e.Location.Y);
                for (int i = 0; i < graph.nodes.Count; i++) {
                    if (graph.pointInsideNode(graph.nodes[i].type, mouseClick, graph.nodes[i].location)) {
                        Graph.Node n = graph.getNodeByLocation(graph.nodes[i].type, mouseClick);
                        n.obstacle = false;
                        if (!isGoalNode){
                            if (startNode.type == Graph.NodeType.NONE) {
                                startNode = n;
                                graph.setNodeColor(startNode.type, g, startNode, Graph.startNodeColor);
                            } else {
                                graph.setNodeColor(startNode.type, g, startNode, startNode.color);
                                startNode = n;
                                graph.setNodeColor(startNode.type, g, startNode, Graph.startNodeColor);
                            }
                            txtStatus.Text = correct + "The starting node has been indicated at " + n.value;
                        }else{
                            if (goalNode.type == Graph.NodeType.NONE) {
                                goalNode = n;
                                graph.setNodeColor(goalNode.type, g, goalNode, Graph.goalNodeColor);
                            } else {
                                graph.setNodeColor(goalNode.type, g, goalNode, goalNode.color);
                                goalNode = n;
                                graph.setNodeColor(goalNode.type, g, goalNode, Graph.goalNodeColor);
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
                Graph.Node n = graph.nodes[i];
                n.obstacle = false;
                graph.nodes[i] = n;
                if (graph.nodes[i].location != goalNode.location && graph.nodes[i].location != startNode.location) 
                    graph.setNodeColor(nodeType, g, graph.nodes[i], sheet.BackColor);
            }
        }

        //  Draw a circle (node)
        private void createNode (int radius, MouseEventArgs e) {
            Point mouseClick = new Point(e.Location.X - (radius / 2), e.Location.Y - (radius / 2));
            
            g.DrawEllipse(pen, mouseClick.X, mouseClick.Y, radius, radius);
            g.DrawString(nodesCounter.ToString(), new Font("Times New Roman", 12), new SolidBrush(Color.Black), new Point(mouseClick.X + 12, mouseClick.Y + 12));
            
            graph.addNode(new Graph.Node(Graph.NodeType.CIRCLE, mouseClick, nodesCounter , 1, sheet.BackColor));
            graph.setNodeColor(Graph.NodeType.CIRCLE, g, graph.nodes[graph.nodes.Count - 1], sheet.BackColor);

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
                        if (graph.pointInsideNode(graph.nodes[i].type, line.firstPoint, graph.nodes[i].location)) {
                            Graph.Node n = graph.getNodeByLocation(graph.nodes[i].type, line.firstPoint);
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
                    if (graph.pointInsideNode(graph.nodes[i].type, line.secondPoint, graph.nodes[i].location)) {
                        Graph.Node n = graph.getNodeByLocation(graph.nodes[i].type, line.secondPoint);
                        if (n.type != Graph.NodeType.NONE) {
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


                            // Trim the line in first node
                            if (graph.nodes[line.firstNodeIndex].location != startNode.location && graph.nodes[line.firstNodeIndex].location != goalNode.location) {
                                graph.setNodeColor(Graph.NodeType.CIRCLE, g, graph.nodes[line.firstNodeIndex], sheet.BackColor);
                            } else if(graph.nodes[line.firstNodeIndex].location == startNode.location){
                                graph.setNodeColor(Graph.NodeType.CIRCLE, g, graph.nodes[line.firstNodeIndex], Graph.startNodeColor);
                            } else {
                                graph.setNodeColor(Graph.NodeType.CIRCLE, g, graph.nodes[line.firstNodeIndex], Graph.goalNodeColor);
                            }

                            // Trim the line in second node
                            if (graph.nodes[line.secondNodeIndex].location != startNode.location && graph.nodes[line.secondNodeIndex].location != goalNode.location) {
                                graph.setNodeColor(Graph.NodeType.CIRCLE, g, graph.nodes[line.secondNodeIndex], sheet.BackColor);
                            } else if (graph.nodes[line.secondNodeIndex].location == startNode.location) {
                                graph.setNodeColor(Graph.NodeType.CIRCLE, g, graph.nodes[line.secondNodeIndex], Graph.startNodeColor);
                            } else {
                                graph.setNodeColor(Graph.NodeType.CIRCLE, g, graph.nodes[line.secondNodeIndex], Graph.goalNodeColor);
                            }

                            
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
            btnAStar.Checked = false;
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


        private void btnAStar_Click (object sender, EventArgs e) {
            uncheckAlgortihmsButtons();
            if (graph.nodes.Count > 1 && graph.edges.Count > 1) {
                btnAStar.Checked = true;
                selectedAlgorithm = Algorithm.AStar;
            } else {
                txtStatus.Text = wrong + "Unable to find connected nodes";
            }
        }



        private void btnThreadControl_Click (object sender, EventArgs e) {
            if (startNode.type != Graph.NodeType.NONE && goalNode.type != Graph.NodeType.NONE) {
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
                        // Indicating costs according to the distance between the goal and the current node.
                        for (int i = 0; i < graph.nodes.Count; i++) {
                            Graph.Node n = graph.nodes[i];
                            if (n.obstacle) 
                                continue;
                            n.cost = graph.distance(graph.nodes[i], goalNode);
                            graph.nodes[i] = n;
                            // Showing the cost
                            if (nodeType == Graph.NodeType.SQUARE)
                                g.DrawString(n.cost.ToString(), new Font("Times New Roman", 8), new SolidBrush(Color.White), n.location);
                            else
                                g.DrawString(n.cost.ToString(), new Font("Times New Roman", 12), new SolidBrush(Color.White), new Point(n.location.X + 48, n.location.Y + 12));
                        }
                        thread = new Thread(() => alg.HillClimbing(startNode, goalNode, seen));
                        txtStatus.Text = "Hill Climbing Starts !";
                    } else if (selectedAlgorithm == Algorithm.AStar) {
                        // Indicating costs according to the distance between the goal and the current node.
                        for (int i = 0; i < graph.nodes.Count; i++) {
                            Graph.Node n = graph.nodes[i];
                            if (n.obstacle)
                                continue;
                            n.cost = graph.distance(graph.nodes[i], goalNode);
                            graph.nodes[i] = n;
                            // Showing the cost
                            if (nodeType == Graph.NodeType.SQUARE)
                                g.DrawString(n.cost.ToString(), new Font("Times New Roman", 8), new SolidBrush(Color.White), n.location);
                            else
                                g.DrawString(n.cost.ToString(), new Font("Times New Roman", 12), new SolidBrush(Color.White), new Point(n.location.X + 48, n.location.Y + 12));
                        }
                        thread = new Thread(() => alg.AStar(startNode, goalNode));
                        txtStatus.Text = "A* Starts !";
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

        private void debug () {
            this.Invoke((MethodInvoker)delegate() {
                txtStatus.Text = "Loading nodes... ";
            });
            String nodes = "";
            int tempNodesCounter = 0, tempEdgesCounter = 0;
            for (int i = 0; i < graph.nodes.Count; i++) {
                nodes += " Node (" + i + "):\n" +
                         "Location: " + graph.nodes[i].location.ToString() + "\n" +
                         "Value: " + graph.nodes[i].value.ToString() + "\n" +
                         "Cost: " + graph.nodes[i].cost.ToString() + "\n" +
                         "Obstacle: " + graph.nodes[i].obstacle.ToString() + "\n";
                tempNodesCounter++;
                for (int j = 0; j < graph.nodes[i].edges.Count; j++) {
                    nodes += "Edge (" + j + "): " + graph.nodes[i].edges[j] + "\n";
                    tempEdgesCounter++;
                }
                this.Invoke((MethodInvoker)delegate() {
                    txtStatus.Text = "Collecting data.. Nodes: " + tempNodesCounter + " | Edges:" + tempEdgesCounter;
                });    
            }
            if (nodes != "") {
                this.Invoke((MethodInvoker)delegate() {
                    txtStatus.Text = "Preparing...";
                });
                debugForm.txtDebug.Text = nodes;
                debugForm.ShowDialog();
                this.Invoke((MethodInvoker)delegate() {
                    txtStatus.Text = "ready !";
                });
                
            } else
                txtStatus.Text = wrong + "Unable to find nodes";
        }

        private void btnDebug_Click (object sender, EventArgs e) {
            new Thread(debug).Start();
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

            btnLine.Enabled = true;
            btnCircle.Enabled = true;

            btnLine.Checked = false;
            btnCircle.Checked = false;
            btnGrid.Checked = false;

            nodeType = Graph.NodeType.CIRCLE;

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

        private void drawGrid(){
            nodeType = Graph.NodeType.SQUARE;
            int squaresCounter = 0;
            for (int x = 0; x < sheet.Width; x += Graph.Node.size.Width){
                for (int y = 0; y < sheet.Height; y += Graph.Node.size.Height) {
                    g.DrawRectangle(pen, new Rectangle(new Point(x, y), new Size(Graph.Node.size.Width, Graph.Node.size.Height)));
                    graph.addNode(new Graph.Node(Graph.NodeType.SQUARE, new Point(x, y), squaresCounter, 1, sheet.BackColor));
                    graph.setNodeColor(nodeType, g, graph.nodes[graph.nodes.Count - 1], sheet.BackColor);
                    squaresCounter++;
                }
            }

            #region Explaining
            /*  Connect Nodes By Edges
             *  Directed Graph Needs: Right (+21)        , Bottom (+1),  
             *                        Right-Diagonal(+20), Ops Left-Diagnaol (+22) 
             *  Undirected Graph Needs: All Directions and all of them are gonna be generated automatically 
             *  after indicating GraphType UNDIRECTED. (Look at addEdge method in Graph class)  
                
             * All Directions are
               graph.addEdge(i, i - 1);        //  top node
               graph.addEdge(i, i + 1);        //  bottom node

               graph.addEdge(i, i + 21);       //  right node
               graph.addEdge(i, i - 21);       //  left node


               graph.addEdge(i, i + 20);       // right-diagnaol node
               graph.addEdge(i, i - 20);       // ops right-diagnaol node

               graph.addEdge(i, i + 22);       // ops left-diagnaol node
               graph.addEdge(i, i - 22);       // left diagnaol node
            */
            #endregion
            for (int i = 0; i < graph.nodes.Count; i++) {
                if (i <= 20) {
                    // left boundary
                    if (i == 20) {
                        // bottom-left corner
                        graph.addEdge(i, i + 21);       // right node
                        graph.addEdge(i, i + 20);       // right-diagnaol node
                    } else {
                        graph.addEdge(i, i + 21);       // right node
                        graph.addEdge(i, i + 1);        // bottom node
                        graph.addEdge(i, i + 22);       // ops left-diagnaol node
                    }
                } else if (i > 840 && i < 860) {
                    // right boundary
                    graph.addEdge(i, i + 1);        // bottom node
                } else if (i % 21 == 0) {
                    // top boundary
                    if (i == 840) {
                        // top-right corner
                        graph.addEdge(i, i + 1);        // bottom node
                    } else {
                        graph.addEdge(i, i + 1);        // bottom node
                        graph.addEdge(i, i + 21);       // right node
                        graph.addEdge(i, i + 22);       // ops left-diagnaol node
                    }
                }else if( (i - 20) % 21 == 0){
                    // bottom boundary
                    if (i == 860)
                        continue;
                    graph.addEdge(i, i + 21);       // right node
                    graph.addEdge(i, i + 20);       // right-diagnaol node    
                } else {
                    // in the center
                    graph.addEdge(i, i + 1);        // bottom node
                    graph.addEdge(i, i + 21);       // right node
                    graph.addEdge(i, i + 20);       // right-diagnaol node   
                    graph.addEdge(i, i + 22);       // ops left-diagnaol node
                }
            }
            
            #region Undirected Graph
            /*  I left this for better understanding to how the nodes are getting connected in UNDIRECTED graph 
                // Undirected Graph
                for (int i = 0; i < graph.nodes.Count; i++) {
                    if (i <= 20) {
                        // left boundary
                        if (i != 0 && i != 20) {
                            // nodes in the center
                            graph.addEdge(i, i - 1);        // top node
                            graph.addEdge(i, i + 1);        // bottom node
                            graph.addEdge(i, i + 20);       // right-diagnaol node
                            graph.addEdge(i, i + 22);       // ops left-diagnaol node
                        }else if (i == 0) {
                            // top-left corner
                            graph.addEdge(i, i + 1);        // bottom node
                            graph.addEdge(i, i + 22);       // ops left-diagnaol node
                        }else if (i == 20) {
                            // bottom-left corner
                            graph.addEdge(i, i - 1);        // top node
                            graph.addEdge(i, i + 20);       // right-diagnaol node
                        }
                        graph.addEdge(i, i + 21);       // right node
                    } else if (i > 840 && i < 860) {
                        // right boundary
                        graph.addEdge(i, i - 1);        //  top node
                        graph.addEdge(i, i + 1);        // bottom node
                        graph.addEdge(i, i - 21);       //  left node
                        graph.addEdge(i, i - 22);       // left diagnaol node
                        graph.addEdge(i, i - 20);       // ops right-diagnaol node
                    } else if (i % 21 == 0) {
                        // top boundary
                        if (i != 0 && i != 840) {
                            graph.addEdge(i, i + 1);        // bottom node
                            graph.addEdge(i, i - 21);       // left node
                            graph.addEdge(i, i + 22);       // ops left-diagnaol node
                            graph.addEdge(i, i - 20);       // ops right-diagnaol node
                        } else if(i == 840){
                            // 840 is the top-right corner
                            graph.addEdge(i, i - 21);       //  left node
                            graph.addEdge(i, i + 1);        // bottom node
                            graph.addEdge(i, i - 20);       // ops right-diagnaol node
                        }
                    } else if ((i - 20) % 21 == 0) {
                        // bottom boundary
                        if (i == 860)
                            continue;
                        graph.addEdge(i, i - 1);        //  top node
                        graph.addEdge(i, i + 21);       //  right node
                        graph.addEdge(i, i - 21);       //  left node
                        graph.addEdge(i, i + 20);       //  right-diagnaol node    
                        graph.addEdge(i, i - 22);       //  left diagnaol node
                    } else {
                        // in the center
                        graph.addEdge(i, i - 1);        //  top node
                        graph.addEdge(i, i + 1);        //  bottom node

                        graph.addEdge(i, i + 21);       //  right node
                        graph.addEdge(i, i - 21);       //  left node

                        graph.addEdge(i, i + 20);       // right-diagnaol node
                        graph.addEdge(i, i - 20);       // ops right-diagnaol node

                        graph.addEdge(i, i + 22);       // ops left-diagnaol node
                        graph.addEdge(i, i - 22);       // left diagnaol node
                    }
                }
            }*/
            #endregion
        }
        private void btnGrid_Click (object sender, EventArgs e) {
            if (deleteGraph("Do you want to delete the current graph ?")) {
                new Thread(drawGrid).Start();
                btnLine.Enabled = false;
                btnCircle.Enabled = false;
            }
        }

        






    }
}
