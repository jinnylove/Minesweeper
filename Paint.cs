using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    static class PaintUtil
    {
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
        static void DrawImage(Graphics g, Bitmap bp)
        {
            g.DrawImage(bp, OffsetX + 5, OffsetY + 5, 24, 24);
        }
        static void PaintTo(this MineArea mineArea, PaintEventArgs e)
        { }
        static void PaintTo(this MineAreaNode mineAreaNode, PaintEventArgs e, bool HasFocus)
        {
            /**
             * not Open
             */
            if (mineAreaNode.state != State.Open)
            {
                g.FillRectangle(HasFocus ? new SolidBrush(Color.FromArgb(100, Color.SandyBrown)) : Brushes.SandyBrown, rectangle);

                switch (mineAreaNode.state)
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

                if (mineAreaNode.HasMine)
                {
                    DrawImage(g, Properties.Resources.Mine);
                }
                else if (mineAreaNode.AroundMineCount != 0)
                {
                    SizeF Size = g.MeasureString(AroundMineCount.ToString(), font);
                    g.DrawString(AroundMineCount.ToString(), font, brushs[AroundMineCount], rectangle);
                }
            }
        }
        static void PaintTo(this Game game, PaintEventArgs e)
        { }

    }
}
