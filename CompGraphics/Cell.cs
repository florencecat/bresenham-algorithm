using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace CompGraphics
{
    class Cell
    {
        private Point cellPosition;
        private int width;
        private bool isPainted;
        
        public bool inArea;
        public Round round;
        public Cell(int i, int j)
        {
            cellPosition.X = j * 20;
            cellPosition.Y = i * 20;

            isPainted = false;
            inArea = false;

            round = null;

            width = 20;
        }
        public void Draw(Graphics graphics, Pen pen)
        {
            graphics.DrawRectangle(pen, cellPosition.X, cellPosition.Y, width, width);
        }

        public void Paint(Graphics graphics, SolidBrush brush)
        {
            isPainted = true;
            graphics.FillRectangle(brush, cellPosition.X, cellPosition.Y, width, width);
        }

        public void AcceptChild(Round child)
        {
            round = child;
        }

        public bool IsPainted() { return isPainted; }
        
    }
}
