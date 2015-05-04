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

        private int DFSCounter;             // used in DFS to check whether it finished or not
        
        public Algorithms (Graphics g,Graph graph, Main frm) {
            this.g = g;
            this.frm = frm;
            this.graph = graph;
            random = new Random();
            this.DFSCounter = 1;
        }

        public void HillClimbing (Graph.Node root, bool[] seen) {
            nodes = new Queue<Graph.Node>();
            nodes.Enqueue(root);
            int localMaximaCounter = 0;
            while (nodes.Count > 0) {
                root = nodes.Dequeue();
                seen[graph.getNodeId(root)] = true;
                Thread.Sleep(500);
                graph.setNodeColor(g, root, Color.Gold);
                frm.Invoke((MethodInvoker)delegate() {
                    frm.txtResult.Text += root.value + " ";
                });

                children = new List<Graph.Node>();
                for (int i = 0; i < root.edges.Count; i++) {
                    Graph.Node n = graph.getNode(root.edges[i]);
                    children.Add(n);
                }

                Graph.Node bestChild = new Graph.Node(new Point(0,0),0,0,999);
                bool found = false;
                for (int i = 0; i < children.Count; i++) {
                    if (!seen[graph.getNodeId(children[i])]) {
                        if (children[i].cost < bestChild.cost && children[i].location != root.location) {
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
                    Thread.Sleep(1000);
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
                frm.txtStatus.Text = " Hill Climbing Finished ! Local Maxima Problem Occured " + localMaximaCounter + " times";
                frm.programStatus = ProgramStatus.STOPPED;
                frm.btnThreadControl.Image = new Bitmap(Properties.Resources.Play);
            });
        }

        public void DFS (Graph.Node root, bool[] seen) {
            seen[graph.getNodeId(root)] = true;
            for (int i = 0; i < root.edges.Count; i++) {
                Graph.Node n = graph.getNode(root.edges[i]);
                if (!seen[root.edges[i]]) {
                    seen[root.edges[i]] = true;
                    DFS(n,seen);
                    Thread.Sleep(1000);
                    graph.setNodeColor(g, n, Color.Gold);
                    frm.Invoke((MethodInvoker)delegate() {
                        frm.txtResult.Text += n.value + " ";
                    });
                    DFSCounter++;
                }
            }
            // Check if DFS visited all the nodes
            if (DFSCounter >= seen.Length) {
                frm.Invoke((MethodInvoker)delegate() {
                    frm.txtStatus.Text = "DFS Finished !";
                    frm.programStatus = ProgramStatus.STOPPED;
                    frm.btnThreadControl.Image = new Bitmap(Properties.Resources.Play);
                });
                DFSCounter = 1;
            }
        }

        public void BFS (Graph.Node root, bool[] seen) {
            nodes = new Queue<Graph.Node>();
            nodes.Enqueue(root);
            seen[graph.getNodeId(root)] = true;
            while (nodes.Count > 0) {
                root = nodes.Dequeue();
                for (int i = 0; i < root.edges.Count; i++) {
                    Graph.Node n = graph.getNode(root.edges[i]);
                    if (!seen[root.edges[i]]) {
                        nodes.Enqueue(n);
                        seen[root.edges[i]] = true;
                        Thread.Sleep(500);
                        graph.setNodeColor(g, n, Color.Gold);
                        
                        frm.Invoke((MethodInvoker)delegate() {
                            frm.txtResult.Text += n.value + " ";
                        });
                    }
                }
            }
            frm.Invoke((MethodInvoker)delegate() {
                frm.txtStatus.Text = " BFS Finished !";
                frm.programStatus = ProgramStatus.STOPPED;
                frm.btnThreadControl.Image = new Bitmap(Properties.Resources.Play);
            });
        }

    }
}
