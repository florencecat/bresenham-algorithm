using System.Drawing;

namespace CompGraphics
{
    class Round
    {
        protected Point cell;
        protected int diameter;
        protected Pen redPen;

        public Cell curCell = null;
        public Round(int x, int y, Cell cCell)
        {
            cell.X = x;
            cell.Y = y;

            diameter = 20;
            curCell = cCell;
            redPen = new Pen(Color.Red);
        }

        public void Draw(Graphics graphics, Pen pen)
        {
            graphics.DrawEllipse(pen, cell.X * diameter, cell.Y * diameter, diameter, diameter);
        }

        public void Select(Graphics graphics)
        {
            graphics.DrawEllipse(redPen, cell.X * diameter, cell.Y * diameter, diameter, diameter);
        }

        public Point GetCell() { return cell; }
    }
}
