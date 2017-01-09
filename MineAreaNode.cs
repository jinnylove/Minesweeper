namespace Minesweeper
{
    internal enum State
    {
        None = 0, Open, Flag, Doubt
    }
    internal class MineAreaNode
    {
   
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
}
