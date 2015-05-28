using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace AI {
    class Algorithms {

        private Graphics g;
        private Graph graph;
        private Main frm;
        private Queue<Graph.Node> nodes;    // used in BFS and Hill Climbing
        private List<Graph.Node> children;  // used in Hill Climbing
        private Random random;

        public int waitingTime;
        
        public Algorithms (Graphics g,Graph graph, Main frm) {
            this.g = g;
            this.frm = frm;
            this.graph = graph;
        
            random = new Random();
            waitingTime = 500;
        }

        public void AStar (Graph.Node root, Graph.Node goal) {
            List<Graph.Node> closedList = new List<Graph.Node>();
            List<Graph.Node> openList = new List<Graph.Node>();
            Graph.Node neighbor;
            int gCost = 0;
            bool foundGoal = false;

            openList.Add(root);
            Graph.Node currentNode;
            while (openList.Count > 0) {
                currentNode = openList[0];
                foreach (Graph.Node node in openList) {
                    if (node.fCost < currentNode.fCost || node.fCost == currentNode.fCost && node.cost < currentNode.cost) {
                        currentNode = node;
                    }
                }

                if (currentNode.location == goal.location) {
                    foundGoal = true;
                    break;
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);
                for (int i = 0; i < currentNode.edges.Count; i++) {
                    neighbor = graph.getNode(currentNode.edges[i]);
                    if (neighbor.obstacle || closedList.Contains(neighbor) || openList.Contains(neighbor)) 
                        continue;

                    gCost = neighbor.gCost + graph.distance(currentNode, neighbor);
                    
                    if (!openList.Contains(neighbor) || gCost < neighbor.gCost) {
                        neighbor.gCost = gCost;
                        neighbor.fCost = neighbor.gCost + neighbor.cost;
                        if (!openList.Contains(neighbor)) {
                            openList.Add(neighbor);
                            
                            if (neighbor.location != root.location && neighbor.location != goal.location)
                                graph.setNodeColor(neighbor.type, g, neighbor, Color.Gold);
                            frm.Invoke((MethodInvoker)delegate() {
                                frm.txtResult.Text += neighbor.fCost + " ";
                            });
                            Thread.Sleep(waitingTime);
                      }
                    }

                }

            }
            frm.Invoke((MethodInvoker)delegate() {
                if (foundGoal) {
                    frm.txtStatus.Text = "A* reached the goal !";
                } else {
                    frm.txtStatus.Text = "A* Finished !";
                }
                frm.programStatus = ProgramStatus.STOPPED;
                frm.btnThreadControl.Image = new Bitmap(Properties.Resources.Play);
            });

        }

        public void HillClimbing (Graph.Node root, Graph.Node goal, bool[] seen) {
            Graph.Node startingNode = root;
            nodes = new Queue<Graph.Node>();
            nodes.Enqueue(root);
            int localMaximaCounter = 0;
            bool foundGoal = false;
            while (nodes.Count > 0) {
                root = nodes.Dequeue();
                seen[graph.getNodeId(root)] = true;
                Thread.Sleep(waitingTime);
                if (root.location != startingNode.location && root.location != goal.location)
                    graph.setNodeColor(root.type, g, root, Color.Gold);
                frm.Invoke((MethodInvoker)delegate() {
                    frm.txtResult.Text += root.value + " ";
                });
                if (root.location == goal.location) {
                    foundGoal = true;
                    break;
                }

                children = new List<Graph.Node>();
                for (int i = 0; i < root.edges.Count; i++) {
                    Graph.Node n = graph.getNode(root.edges[i]);
                    children.Add(n);
                }

                Graph.Node bestChild = new Graph.Node(root.type, new Point(0, 0), 0, 9999, Color.Black);
                bool found = false;
                for (int i = 0; i < children.Count; i++) {
                    if (!seen[graph.getNodeId(children[i])]) {
                        // This condition is not part of the algorithm I just added it to improve it
                        // It checks whether the goal is one of node's children or not
                        if (children[i].location == goal.location)
                            bestChild = children[i];
                        else if (children[i].cost < bestChild.cost && children[i].location != root.location) {
                            bestChild = children[i];
                        }
                        found = true;
                    }
                }
                
                if (found) {
                    nodes.Enqueue(bestChild);
                }else {
                    // Local Maxima Occured, pick a random node
                    localMaximaCounter++;
                    frm.Invoke((MethodInvoker)delegate() {
                        frm.txtStatus.Text = "Solving Local Maxima for " + localMaximaCounter + " time";
                    });
                    Thread.Sleep(waitingTime);
                    int nextNodeId = 0;
                    int counter = 0;
                    bool foundNode = false;
                    // TODO: Improve this part 
                    while(true){
                        nextNodeId = random.Next(graph.nodes.Count - 1);
                        if (!seen[graph.getNodeId(graph.nodes[nextNodeId])]) {
                            foundNode = true;
                            break;
                        }
                        counter++;
                        if(counter > seen.Length)
                            break;
                    }
                    if(foundNode)
                        nodes.Enqueue(graph.nodes[nextNodeId]);
                }
                 
            }
            frm.Invoke((MethodInvoker)delegate() {
                if (foundGoal) {
                    frm.txtStatus.Text = "Hill Climbing reached the goal ! Local Maxima Problem Occured " + localMaximaCounter + " times";
                } else {
                    frm.txtStatus.Text = " Hill Climbing Finished ! Local Maxima Problem Occured " + localMaximaCounter + " times";
                }
                frm.programStatus = ProgramStatus.STOPPED;
                frm.btnThreadControl.Image = new Bitmap(Properties.Resources.Play);
            });
        }

        public void DFS (Graph.Node root, Graph.Node goal, bool[] seen) {
            seen[graph.getNodeId(root)] = true;
            for (int i = 0; i < root.edges.Count; i++) {
                Graph.Node n = graph.getNode(root.edges[i]);
                if (!seen[root.edges[i]]) {
                    seen[root.edges[i]] = true;
                    if (n.location != root.location && n.location != goal.location && !n.obstacle)
                        graph.setNodeColor(n.type, g, n, Color.Gold);
                    Thread.Sleep(waitingTime);
                    
                    frm.txtResult.Text += n.value + " ";
                    if (n.location == goal.location) {
                        frm.txtStatus.Text = "DFS reached the goal !";
                        frm.programStatus = ProgramStatus.STOPPED;
                        frm.btnThreadControl.Image = new Bitmap(Properties.Resources.Play);
                    }
                    DFS(n, goal,seen);
                    if (frm.programStatus == ProgramStatus.STOPPED)
                        return;
                    
                    

                }
                   
                    
                    
            }
        }

        public void BFS (Graph.Node root, Graph.Node goal, bool[] seen) {
            nodes = new Queue<Graph.Node>();
            nodes.Enqueue(root);
            seen[graph.getNodeId(root)] = true;
            bool foundGoal = false;
            while (nodes.Count > 0) {
                root = nodes.Dequeue();
                for (int i = 0; i < root.edges.Count; i++) {
                    Graph.Node n = graph.getNode(root.edges[i]);
                    if (!seen[root.edges[i]]) {
                        nodes.Enqueue(n);
                        seen[root.edges[i]] = true;
                        Thread.Sleep(waitingTime);
                        if (n.location != root.location && n.location != goal.location && !n.obstacle)
                            graph.setNodeColor(n.type, g, n, Color.Gold);
                        frm.Invoke((MethodInvoker)delegate() {
                            frm.txtResult.Text += n.value + " ";
                        });
                        if (n.location == goal.location) {
                            foundGoal = true;
                            break;
                        }
                    }
                }
                if (foundGoal)
                    break;
            }
            frm.Invoke((MethodInvoker)delegate() {
                if(foundGoal)
                    frm.txtStatus.Text = "BFS reached the goal !";
                else
                    frm.txtStatus.Text = " BFS Finished !";
                frm.programStatus = ProgramStatus.STOPPED;
                frm.btnThreadControl.Image = new Bitmap(Properties.Resources.Play);
            });
        }

    }
}
