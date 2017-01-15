using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form_Main : Form
    {
        Game game;

        public Form_Main()
        {
            InitializeComponent();
            //DoubleBuffered = true;
            game = new Game();
        }

        private void Form_Main_Paint()
        {
            Graphics g = CreateGraphics();
            game.OnPaint(g);
            g.Dispose();
        }

        private void Form_Main_MouseUp(object sender, MouseEventArgs e)
        {
            game.OnMouseClick(e);
            Form_Main_Paint();
        }

        private void Form_Main_Paint(object sender, PaintEventArgs e)
        {
            Form_Main_Paint();
        }
    }
}
