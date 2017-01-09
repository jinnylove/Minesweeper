using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    class Util
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
    }
}
