using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace AI {
    public class Graph {
        public struct Node {

            public int radius;
            public Point location;

            public int value;
            public List<int> edges;
            
            public Node (Point location, int value, int radius) {
                this.location = location;
                this.value = value;
                this.radius = radius;
                this.edges = new List<int>();
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

        public Graph () {
            nodes = new List<Node>();
            edges = new List<Edge>();
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
            nodes[second_node].edges.Add(first_node);
        }

        public Node getNode (int index) {
            return nodes[index];
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


        public Node getNodeByLocation (Point location) {
            for (int i = 0; i < nodes.Count; i++) {
                if (pointInsideNode(location,nodes[i].location,nodes[i].radius)){
                    return nodes[i];
                }
            }
            return new Node(new Point(0, 0), 0, 0);
        }

        public void setNodeColor (Graphics g, Node node, Color color) {
            g.FillEllipse(new SolidBrush(color), node.location.X, node.location.Y, node.radius, node.radius);
            g.DrawString(node.value.ToString(), new Font("Times New Roman", 12), new SolidBrush(Color.Black), new Point(node.location.X + 12, node.location.Y + 12));
        }

        public bool pointInsideNode (Point point, Point nodeLocation, int radius) {
            return (point.X - nodeLocation.X) * (point.X - nodeLocation.X) +
                   (point.Y - nodeLocation.Y) * (point.Y - nodeLocation.Y) < radius * radius;
        }

    }
}
