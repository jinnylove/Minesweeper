using System;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    internal class MineArea
    {
    
        public int RowCount { get; private set; }
        public int ColumeCount { get; private set; }
        public int MineCount { get; private set; }

        internal MineArea(int RowCount, int ColumeCount, int MineCount)
        {
            Set(RowCount, ColumeCount, MineCount);
        }

        public void Set(int RowCount, int ColumeCount, int MineCount)
        {
            this.RowCount = RowCount;
            this.ColumeCount = ColumeCount;
            this.MineCount = MineCount;
        }

        public void Set(Level level)
        {
            switch (level)
            {
                case Level.Beginner:
                    Set(10, 10, 10);
                    break;
                case Level.Intermediate:
                    Set(16, 16, 40);
                    break;
                case Level.Expert:
                    Set(30, 16, 99);
                    break;
            }
        }

        public Level level
        {
            get
            {
                if (Is(10, 10, 10))
                {
                    return Level.Beginner;
                }
                else if (Is(16, 16, 40))
                {
                    return Level.Intermediate;
                }
                else if (Is(30, 16, 99))
                {
                    return Level.Expert;
                }
                return Level.Setting;
            }
        }

        internal bool Is(int RowCount, int ColumeCount, int MineCount)
        {
            return this.RowCount == RowCount
                && this.ColumeCount == ColumeCount
                && this.MineCount == MineCount;
        }

        /**
         * About State
         */
        class NodeArea
        {
            enum State
            {
                None = 0, Open, Flag, Doubt
            }
            State state;
            void UpdateState(ref State s)
            {
                switch (s)
                {
                    case State.Open:
                        s = State.Open;
                        break;
                    case State.None:
                        s = State.Flag;
                        break;
                    case State.Flag:
                        s = State.Doubt;
                        break;
                    case State.Doubt:
                        s = State.None;
                        break;
                }
            }
            bool HasMine;
            int AroundMineCount;
            int OffsetX;
            int OffsetY;

            RectangleF rectangle;

            static Font font = new Font("Consolas", 16);
            static Brush[] brushs = new Brush[]
            {
                    null,
                    new SolidBrush(Color.Blue),
                    new SolidBrush(Color.Green),
                    new SolidBrush(Color.Red),
                    new SolidBrush(Color.DarkBlue),
                    new SolidBrush(Color.DarkRed),
                    new SolidBrush(Color.DarkSeaGreen),
                    new SolidBrush(Color.Black),
                    new SolidBrush(Color.DarkGray)
            };

            void DrawImage(Graphics g, Bitmap bp)
            {
                g.DrawImage(bp, OffsetX + 5, OffsetY + 5, 24, 24);
            }

            internal void PaintTo(Graphics g, bool HasFocus)
            {
                /**
                 * not Open
                 */
                if (state != State.Open)
                {
                    g.FillRectangle(HasFocus ? new SolidBrush(Color.FromArgb(100, Color.SandyBrown)) : Brushes.SandyBrown, rectangle);

                    switch (state)
                    {
                        case State.Flag: DrawImage(g, Properties.Resources.Flag); break;
                        case State.Doubt: DrawImage(g, Properties.Resources.Doubt); break;
                    }
                }
                /**
                 * Open
                 */
                else
                {
                    g.FillRectangle(HasFocus ? new SolidBrush(Color.FromArgb(100, Color.LightGray)) : Brushes.LightGray, rectangle);

                    if (HasMine)
                    {
                        DrawImage(g, Properties.Resources.Mine);
                    }
                    else if (AroundMineCount != 0)
                    {
                        SizeF Size = g.MeasureString(AroundMineCount.ToString(), font);
                        g.DrawString(AroundMineCount.ToString(), font, brushs[AroundMineCount], rectangle);
                    }
                }
            }
        }

        internal void OnMouseUp(MouseEventArgs e)
        {
        }

        internal void OnMouseLeave()
        {
            throw new NotImplementedException();
        }

        internal void OnMouseMove(MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        internal void clear()
        {
            throw new NotImplementedException();
        }

        NodeArea[,] NodeAreas;


        /**
         * About Paint
         */

        const int OffsetX = 6;
        const int OffSetY = 6;

        internal int Width
        {
            get
            {
                return 2 * OffsetX + 34 * RowCount;
            }
        }

        internal int Height
        {
            get
            {
                return 2 * OffSetY + 34 * ColumeCount;
            }
        }

        Point MouseOutOfFocus = new Point(-1, -1);
        Point MouseFocus;

        internal void PaintTo(PaintEventArgs g)
        {
            g.Graphics.Clear(Color.White);

            foreach (NodeArea nodeArea in NodeAreas)
            {
                nodeArea.PaintTo(g.Graphics, false);
            }
        }

        Point GetPoint(int x, int y)
        {
            if (x < OffsetX || x > Width - OffsetX || y < OffSetY || y > Width - OffSetY)
            {
                return new Point();
            }
            return new Point((x - OffsetX) / 34, (y - OffSetY) / 34);
        }

        internal void OnMouseMove(int x, int y)
        {
            MouseFocus = GetPoint(x, y);
        }
    }
}
