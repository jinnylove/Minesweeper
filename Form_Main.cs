using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form_Main : Form
    {
        Game game;

        public Form_Main()
        {
            InitializeComponent();
            DoubleBuffered = true;
            game = new Game(100,100,100);
        }

        private void Form_Main_Paint(object sender, PaintEventArgs e)
        {
            game.PaintTo(e);
        }
    }
}
