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

            game.SetClock(new Clock(Timer_Main, Label_Timer));
            game.SetMineC(new MineC(Label_Mine));

            markMToolStripMenuItem.Checked = Properties.Settings.Default.Mark;
            game.SetMarkable(markMToolStripMenuItem.Checked);

            audioMToolStripMenuItem.Checked = Properties.Settings.Default.Audio;
            game.SetAudioable(audioMToolStripMenuItem.Checked);

            game.SetTickClockSound(Properties.Resources.TickingClockSound);
        }

        /**
         * Begin Game on Load
         */

        private void Form_Main_Load(object sender, EventArgs e)
        {
            BeginNewGame(Level.Setting);
        }

        /**
         * Refresh
         */

        private void Form_Main_Paint(object sender, PaintEventArgs e)
        {
            game.PaintTo(e);
        }

        /**
         * ToolStripMenuItem_Click
         */
   
        private void newGameNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeginNewGame(Level.Setting);
        }

        private void beginnerBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeginNewGame(Level.Beginner);
        }

        private void intermediateIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeginNewGame(Level.Intermediate);
        }

        private void expertEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeginNewGame(Level.Expert);
        }

        private void settingSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form_Setting().ShowDialog();
            BeginNewGame(Level.Setting);
        }

        private void markMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            markMToolStripMenuItem.Checked = !markMToolStripMenuItem.Checked;
            game.UpdateMarkable(markMToolStripMenuItem.Checked);
        }

        private void audioMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            audioMToolStripMenuItem.Checked = !audioMToolStripMenuItem.Checked;
            game.UpdateAudioable(audioMToolStripMenuItem.Checked);
        }

        private void rankRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form_Rank().ShowDialog();
        }

        private void exitXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to exit the game?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        [DllImport("shell32.dll")]
        public extern static int ShellAbout(IntPtr hWnd, string szApp, string szOtherStuff, IntPtr hIcon);
        private void aboutAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShellAbout(this.Handle, "Minesweeper", "A minesweeper game using CSharp language.", this.Icon.Handle);
        }  
    
        /**
         * Clock
         */
    
        private void Timer_Main_Tick(object sender, EventArgs e)
        {
            game.OnClockTick();
        }
    
        /**
         * Mouse
         */
    
        private void Form_Main_MouseMove(object sender, MouseEventArgs e)
        {
            Refresh();
        }
        private void Form_Main_MouseLeave(object sender, EventArgs e)
        {
            Refresh();
        }    
    
        /**
         * Utils
         */
    
        private void BeginNewGame(Level NewLevel)
        {
            GetToolStripMenuItem(game.level).Checked = false;
            if (NewLevel == Level.Setting)
            {
                game.SetLevel(Properties.Settings.Default.RowCount, Properties.Settings.Default.ColumeCount, Properties.Settings.Default.MineCount);
            }
            else
            {
                game.SetLevel(NewLevel);
            }
            GetToolStripMenuItem(game.level).Checked = true;

            UpdateClientSize();
            game.BeginNewGame();
        }

        private void UpdateClientSize()
        {
            Size size = game.MineAreaSize;
            size.Height += MainMenuStrip.Height;
            size.Height += TableLayoutPanel_Main.Height;
            ClientSize = size;
        }

        private ToolStripMenuItem GetToolStripMenuItem(Level level)
        {
            switch (level)
            {
                case Level.Beginner:
                    return beginnerBToolStripMenuItem;
                case Level.Intermediate:
                    return intermediateIToolStripMenuItem;
                case Level.Expert:
                    return expertEToolStripMenuItem;
                case Level.Setting:
                    return settingSToolStripMenuItem;
                default:
                    return settingSToolStripMenuItem;
            }
        }
    }
}
