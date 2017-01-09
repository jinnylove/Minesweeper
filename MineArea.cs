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
        internal MineArea() { }
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
