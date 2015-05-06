using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace AI {
    public class Graph {

        public enum NodeType { NONE, CIRCLE, SQUARE };

        public struct Node {

            static public int radius = 40;
            static public Size size = new Size(20,20);
            public Point location;

            public int value;
            public List<int> edges;

            public int cost;

            public Color color;

            public NodeType type;

            public bool obstacle;

            
            public Node (NodeType nodeType, Point location, int value, int cost, Color color) {
                this.edges = new List<int>();
                this.type = nodeType;
                this.location = location;
                this.value = value;
                this.cost = cost;
                this.color = color;
                this.obstacle = false;
            }
            
        }
        public struct Edge {
            public int first;
            public int second;
            public Edge (int first, int second) {
                this.first = first;
                this.second = second;
            }
        }

        public List<Node> nodes;
        public List<Edge> edges;

        static public Color startNodeColor = Color.LightBlue;
        static public Color goalNodeColor = Color.Red;
        static public Color obstacleColor = Color.Black;

        private bool directed;

        public Graph (bool directed) {
            nodes = new List<Node>();
            edges = new List<Edge>();
            this.directed = directed;
        }

        ~Graph () {
            if (nodes.Count > 0)
                nodes.RemoveRange(0,nodes.Count-1);
            if (edges.Count > 0)
                edges.RemoveRange(0, edges.Count - 1);
        }

        public void addNode (Node node) {
            nodes.Add(node);
        }

        public void addEdge (int first_node, int second_node) {
            edges.Add(new Edge(first_node, second_node));
            nodes[first_node].edges.Add(second_node);
            if(!directed)
                nodes[second_node].edges.Add(first_node); 
        }

        public Node getNode (int index) {
            return nodes[index];
        }


        public int distance (Node first, Node second) {
            Point vec = new Point((second.location.X - first.location.X),(second.location.Y - first.location.Y));
            return (int)Math.Sqrt((vec.X * vec.X) + (vec.Y * vec.Y));
        }

        public int getNodeId (Node node) {
            for (int i = 0; i < nodes.Count; i++) {
                if (node.location == nodes[i].location)
                    return i;
            }
            return -1;
        }

        public Edge getEdge (int index) {
            return edges[index];
        }

        public Node getNodeByLocation (NodeType type, Point location) {
            for (int i = 0; i < nodes.Count; i++) {
                if (pointInsideNode(type, location, nodes[i].location)) {
                    return nodes[i];
                }
            }
            return new Node(NodeType.NONE, new Point(0, 0), 0, 0,Color.Black);
        }


        public void setNodeColor (NodeType type, Graphics g, Node node, Color color) {
            if (type == NodeType.CIRCLE) {
                g.FillEllipse(new SolidBrush(color), node.location.X, node.location.Y, Node.radius, Node.radius);
                g.DrawString(node.value.ToString(), new Font("Times New Roman", 12), new SolidBrush(Color.Black), new Point(node.location.X + 12, node.location.Y + 12));
            } else {
                // Square Node
                g.FillRectangle(new SolidBrush(color), new Rectangle(node.location, new Size(Node.size.Width - 1 , Node.size.Height - 1)));
            }
        }

        

        // Collision detection
        public bool pointInsideNode (NodeType nodeType, Point point, Point nodeLocation) {
            if (nodeType == NodeType.SQUARE) {
                // our zero at top left corner
                Point topRight = new Point(nodeLocation.X + Node.size.Width, nodeLocation.Y);
                Point bottomLeft = new Point(nodeLocation.X, nodeLocation.Y + Node.size.Height);
                return point.X >= bottomLeft.X &&
                        point.X <= topRight.X &&
                        point.Y >= topRight.Y &&
                        point.Y <= bottomLeft.Y;
            } else {
                // Circle
                return (point.X - nodeLocation.X) * (point.X - nodeLocation.X) +
                       (point.Y - nodeLocation.Y) * (point.Y - nodeLocation.Y) < Node.radius * Node.radius;
            }
        }

        public void freeMemory () {
            if (nodes.Count > 0)
                nodes.RemoveRange(0, nodes.Count - 1);
            if (edges.Count > 0)
                edges.RemoveRange(0, edges.Count - 1);
            
        }

    }
}
