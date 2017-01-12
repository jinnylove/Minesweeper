using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form_Main : Form
    {
        Game game;

        /**
         * init
         */

        public Form_Main()
        {
            InitializeComponent();

            DoubleBuffered = true;

            game = new Game();

            
        }

        /**
         * Refresh
         */

        private void Form_Main_Paint(object sender, PaintEventArgs e)
        {
            game.PaintTo(e);
        }
    }
}
