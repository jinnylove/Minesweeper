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

        internal static void PaintTo(this Game game, PaintEventArgs e)
        { }
        internal static void PaintTo(this MineArea mineArea, PaintEventArgs e)
        { }
        internal static void PaintTo(this MineAreaNode mineAreaNode, PaintEventArgs e, bool HasFocus)
        {
            Graphics g = e.Graphics;
            /**
             * not Open
             */
            if (mineAreaNode.state != State.Open)
            {
                g.FillRectangle(HasFocus ? new SolidBrush(Color.FromArgb(100, Color.SandyBrown)) : Brushes.SandyBrown, mineAreaNode.g);


            }
            /**
             * Open
             */
            else
            {
                g.FillRectangle(HasFocus ? new SolidBrush(Color.FromArgb(100, Color.LightGray)) : Brushes.LightGray, mineAreaNode.g);

   
                //else if (mineAreaNode.AroundMineCount != 0)
                {
                    SizeF Size = g.MeasureString(mineAreaNode.AroundMineCount.ToString(), font);
                    g.DrawString(mineAreaNode.AroundMineCount.ToString(), font, brushs[mineAreaNode.AroundMineCount], mineAreaNode.g);
                }
            }
        }
       

    }
}
