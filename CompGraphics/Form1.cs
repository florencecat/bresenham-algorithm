using System;
using System.Drawing;
using System.Windows.Forms;

namespace CompGraphics
{
    
    public partial class Form1 : Form
    {
        const int length = 32, width = 32;
        private Cell[,] cells = new Cell[length, width];
        private Round[] rounds = new Round[4];

        private Round currenRound = null;

        private Graphics graphics;
        private Pen matrixPen = new Pen(Color.Black, 1);
        private Pen roundPen = new Pen(Color.Black, 2);
        private SolidBrush cellBrush = new SolidBrush(Color.Wheat);

        public Form1()
        {
            InitializeComponent();

            graphics = panel1.CreateGraphics();

            
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++) 
                {
                    cells[i, j] = new Cell(i, j);
                    cells[i, j].Draw(graphics, matrixPen);
                }
            }

            rounds[0] = new Round(0, 0, cells[0, 0]);
            cells[0, 0].AcceptChild(rounds[0]);

            rounds[1] = new Round(length - 1, width - 1, cells[length - 1, width - 1]);
            cells[length - 1, width - 1].AcceptChild(rounds[1]);

            rounds[2] = new Round(width - 1, 0, cells[0, width - 1]);
            cells[0, width - 1].AcceptChild(rounds[2]);

            rounds[3] = new Round(0, length - 1, cells[length - 1, 0]);
            cells[length - 1, 0].AcceptChild(rounds[3]);

            cellBrush = new SolidBrush(Color.GreenYellow);

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if ((i >= rounds[2].GetCell().Y && i <= rounds[3].GetCell().Y) && (j >= rounds[3].GetCell().X && j <= rounds[2].GetCell().X))
                    {
                        cells[i, j].inArea = true;
                        cells[i, j].Paint(graphics, cellBrush);
                    }
                    else
                        cells[i, j].inArea = false;

                    cells[i, j].Draw(graphics, matrixPen);
                }
            }

            Bresenham();
        }     
        
        private void Bresenham()
        {
            cellBrush = new SolidBrush(Color.Wheat);

            int x1 = rounds[0].GetCell().X, y1 = rounds[0].GetCell().Y;
            int x2 = rounds[1].GetCell().X, y2 = rounds[1].GetCell().Y;

            int deltaX = Math.Abs(x2 - x1);
            int deltaY = Math.Abs(y2 - y1);
            int signX = x1 < x2 ? 1 : -1;
            int signY = y1 < y2 ? 1 : -1;
            int error = deltaX - deltaY;

            if (cells[y2, x2].inArea == true)
            {
                cells[y2, x2].Paint(graphics, cellBrush);
                cells[y2, x2].Draw(graphics, matrixPen);
            }

            while (x1 != x2  || y1 != y2)
            {
                if (cells[y1, x1].inArea == true)
                {
                    cells[y1, x1].Paint(graphics, cellBrush);
                    cells[y1, x1].Draw(graphics, matrixPen);
                }
                else
                {
                    cellBrush = new SolidBrush(Color.White);

                    cells[y1, x1].Paint(graphics, cellBrush);
                    cells[y1, x1].Draw(graphics, matrixPen);

                    cellBrush = new SolidBrush(Color.Wheat);
                }
                
                int error2 = error * 2;
                if (error2 > -deltaY)
                {
                    error -= deltaY;
                    x1 += signX;
                }
                if (error2 < deltaX)
                {
                    error += deltaX;
                    y1 += signY;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                rounds[i].Draw(graphics, roundPen);
            }

            graphics.DrawLine(roundPen, rounds[0].GetCell().X * 20, rounds[0].GetCell().Y * 20, rounds[1].GetCell().X * 20, rounds[1].GetCell().Y * 20);
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (currenRound == null)
            {
                if (cells[e.Y / 20, e.X / 20].round != null)
                {
                    cells[e.Y / 20, e.X / 20].round.Select(graphics);
                    currenRound = cells[e.Y / 20, e.X / 20].round;
                }
            }
            else
            {
                for (int i = 0; i < 4; i++) 
                {
                    if(rounds[i] == currenRound)
                    {
                        rounds[i].curCell.round = null;
                        rounds[i] = new Round(e.X / 20, e.Y / 20, cells[e.Y / 20, e.X / 20]);
                        cells[e.Y / 20, e.X / 20].round = rounds[i];
                    }
                }

                currenRound = null;
                Clear(false);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clear(true);
        }

        private void Clear(bool deepCleaning)
        {

            cellBrush = new SolidBrush(Color.White);

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    cells[i, j].Paint(graphics, cellBrush);
                    if (deepCleaning == true)
                        cells[i, j] = new Cell(i, j);
                    cells[i, j].Draw(graphics, matrixPen);
                }
            }

            if (deepCleaning == true)
            {
                rounds[0] = new Round(0, 0, cells[0, 0]);
                cells[0, 0].AcceptChild(rounds[0]);

                rounds[1] = new Round(length - 1, width - 1, cells[length - 1, width - 1]);
                cells[length - 1, width - 1].AcceptChild(rounds[1]);

                rounds[2] = new Round(width - 1, 0, cells[0, width - 1]);
                cells[0, width - 1].AcceptChild(rounds[2]);

                rounds[3] = new Round(0, length - 1, cells[length - 1, 0]);
                cells[length - 1, 0].AcceptChild(rounds[3]);

                rounds[0].Draw(graphics, matrixPen);
                rounds[1].Draw(graphics, matrixPen);
                rounds[2].Draw(graphics, matrixPen);
                rounds[3].Draw(graphics, matrixPen);
            }

            cellBrush = new SolidBrush(Color.GreenYellow);

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if ((i >= rounds[2].GetCell().Y && i <= rounds[3].GetCell().Y) && (j >= rounds[3].GetCell().X && j <= rounds[2].GetCell().X))
                    {
                        cells[i, j].inArea = true;
                        cells[i, j].Paint(graphics, cellBrush);
                    }
                    else
                        cells[i, j].inArea = false;

                    cells[i, j].Draw(graphics, matrixPen);
                }
            }

            Bresenham();
        }
    }
}
