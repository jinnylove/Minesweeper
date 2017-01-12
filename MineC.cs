using System;
using System.Windows.Forms;

namespace Minesweeper
{
    internal class MineC
    {
        Label MineLabel;
        internal MineC(Label label)
        {
            MineLabel = label;
        }
        internal int MineCount
        {
            get
            {
                return Convert.ToInt32(MineLabel.Text);
            }
            set
            {
                MineLabel.Text = value.ToString();
            }
        }
    }
}
