using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AI {
    class Lines {
        private Graphics g;
        public List<Line> lines;

        public struct Line {
            private Graphics g;

            public Point firstPoint;
            public Point secondPoint;

            public int firstNodeIndex;
            public int secondNodeIndex;

            public const int pointSize = 4;

            public bool gotFirstPoint;
            
            public Line (Graphics g) {
                this.g = g;
                firstPoint = secondPoint = new Point(0, 0);
                gotFirstPoint = false;
                firstNodeIndex = secondNodeIndex = 0;
            }
            public void draw (Pen pen, Point firstPoint, Point secondPoint, int firstNodeIndex,int secondNodeIndex) {
                g.DrawLine(pen, firstPoint, secondPoint);
                this.firstNodeIndex = firstNodeIndex;
                this.secondNodeIndex= secondNodeIndex;
            }
        }

        
        public Lines (Graphics g) {
            this.g = g;
            lines = new List<Line>();
        }

        public void add (Line line) {
            lines.Add(line);
        }

        public void delete (int index) {
            g.DrawLine(new Pen(Color.Transparent), lines[index].firstPoint, lines[index].secondPoint);
        }

        public void delete (Line line) {
            for (int i = 0; i < lines.Count; i++) {
                if (lines[i].firstPoint == line.firstPoint && lines[i].secondPoint == line.secondPoint) {
                    g.DrawLine(new Pen(Color.Transparent), lines[i].firstPoint, lines[i].secondPoint);
                    return;
                }
            }       
        }

        public void freeMemory () {
            if(lines.Count > 0)
                lines.RemoveRange(0,lines.Count-1);
        }


    }

}