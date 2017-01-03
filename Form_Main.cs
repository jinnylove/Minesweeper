using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form_Main : Form
    {
        public int nWidth { private set; get; }
        public int nHeight { private set; get; }
        public int nMineCnt { private set; get; }

        private void Setn(int Width, int Height, int MineCnt)
        {
            nWidth = Width;
            nHeight = Height;
            nMineCnt = MineCnt;
        }

        private bool Isn(int Width, int Height, int MineCnt)
        {
            return nWidth == Width && nHeight == Height && nMineCnt == MineCnt;
        }

        private enum Level
        {
            Beginner, Intermediate, Expert, Setting
        }

        private Level level
        {
            get
            {
                if (Isn(10, 10, 10))
                {
                    return Level.Beginner;
                }
                else if (Isn(16, 16, 40))
                {
                    return Level.Intermediate;
                }
                else if (Isn(30, 16, 99))
                {
                    return Level.Expert;
                }
                return Level.Setting;
            }
            set
            {
                switch (value)
                {
                    case Level.Beginner:
                        Setn(10, 10, 10);
                        break;
                    case Level.Intermediate:
                        Setn(16, 16, 40);
                        break;
                    case Level.Expert:
                        Setn(30, 16, 99);
                        break;
                    case Level.Setting:
                        break;
                }
            }
        }

        private void UpdateGUI()
        {
            int nOffsetX = this.Width - this.ClientSize.Width;
            int nOffsetY = this.Height - this.ClientSize.Height;
            int nAdditionY = MenuStrip_Main.Height + TableLayoutPanel_Main.Height;
            this.Width = 12 + 34 * nWidth + nOffsetX;
            this.Height = 12 + 34 * nHeight + nAdditionY + nOffsetY;

            beginnerBToolStripMenuItem.Checked = false;
            intermediateIToolStripMenuItem.Checked = false;
            expertEToolStripMenuItem.Checked = false;
            settingSToolStripMenuItem.Checked = false;

            switch (level)
            {
                case Level.Beginner:
                    beginnerBToolStripMenuItem.Checked = true;
                    break;
                case Level.Intermediate:
                    intermediateIToolStripMenuItem.Checked = true;
                    break;
                case Level.Expert:
                    expertEToolStripMenuItem.Checked = true;
                    break;
                case Level.Setting:
                    settingSToolStripMenuItem.Checked = true;
                    break;
            }
        }

        System.Media.SoundPlayer soundTick;
        //System.Media.SoundPlayer soundBomb;

        public Form_Main()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            Setn(Properties.Settings.Default.Width,
                Properties.Settings.Default.Height,
                Properties.Settings.Default.MineCnt);
            markMToolStripMenuItem.Checked = Properties.Settings.Default.Mark;
            audioMToolStripMenuItem.Checked = Properties.Settings.Default.Audio;
            soundTick = new System.Media.SoundPlayer(Properties.Resources.Ticking_clock_sound);
            UpdateGUI();
            BeginNewGame();
        }

        private void Form_Main_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);
            int nOffsetX = 6;
            int nOffsetY = 6 + MenuStrip_Main.Height;
            for (int i = 0; i < nWidth; i++)
            {
                for (int j = 0; j < nHeight; j++)
                {   
                    if(pState[i, j] != State.Open)
                    {
                        if (i == MouseFocus.X && j == MouseFocus.Y)
                        {
                            g.FillRectangle(new SolidBrush(Color.FromArgb(100, Color.SandyBrown)), new Rectangle(nOffsetX + 34 * i + 1, nOffsetY + 34 * j + 1, 32, 32));
                        }
                        else
                        {
                            g.FillRectangle(Brushes.SandyBrown, new Rectangle(nOffsetX + 34 * i + 1, nOffsetY + 34 * j + 1, 32, 32));
                        }
                    }
                    else
                    {
                        if (MouseFocus.X == i && MouseFocus.Y == j)
                        {
                            g.FillRectangle(new SolidBrush(Color.FromArgb(100, Color.LightGray)), new Rectangle(nOffsetX + 34 * i + 1, nOffsetY + 34 * j + 1, 32, 32));
                        }
                        else
                        {
                            g.FillRectangle(Brushes.LightGray, new Rectangle(nOffsetX + 34 * i + 1, nOffsetY + 34 * j + 1, 32, 32));

                        }
                    }
                    switch (pState[i, j])
                    {
                        case State.Open:
                            if (!pHasMine[i, j])
                            {
                                int MineAround = Around(pHasMine, i, j).Count(k => k);
                                
                                if (MineAround != 0)
                                {
                                    Color? color = null;
                                    switch (MineAround)
                                    {
                                        case 1:
                                            color = Color.Blue;
                                            break;
                                        case 2:
                                            color = Color.Green;
                                            break;
                                        case 3:
                                            color = Color.Red;
                                            break;
                                        case 4:
                                            color = Color.DarkBlue;
                                            break;
                                        case 5:
                                            color = Color.DarkRed;
                                            break;
                                        case 6:
                                            color = Color.DarkSeaGreen;
                                            break;
                                        case 7:
                                            color = Color.Black;
                                            break;
                                        case 8:
                                            color = Color.DarkGray;
                                            break;
                                    }
                                    Brush DrawBrush = new SolidBrush(color ?? Color.Blue);
                                    SizeF Size = g.MeasureString(MineAround.ToString(), new Font("Consolas", 16));
                                    g.DrawString(MineAround.ToString(), new Font("Consolas", 16), DrawBrush,
                                        nOffsetX + 34 * i + 1 + (32 - Size.Width) / 2,
                                        nOffsetY + 34 * j + 1 + (32 - Size.Height) / 2);
                                }
                            }
                            else
                            {
                                g.DrawImage(Properties.Resources.Mine, nOffsetX + 34 * i + 1 + 4, nOffsetY + 34 * j + 1 + 2, 24, 24);
                            }
                            break;
                        case State.Flag:
                            g.DrawImage(Properties.Resources.Flag, nOffsetX + 34 * i + 1 + 4, nOffsetY + 34 * j + 1 + 2, 24, 24);
                            break;
                        case State.Doubt:
                            g.DrawImage(Properties.Resources.Doubt, nOffsetX + 34 * i + 1 + 4, nOffsetY + 34 * j + 1 + 2, 24, 24);
                            break;
                    }
                }
            }
        }

        private void beginnerBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            level = Level.Beginner;
        }

        private void intermediateIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            level = Level.Intermediate;
        }

        private void expertEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            level = Level.Expert;
        }

        private void exitXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure to exit the game?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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

        private void markMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            markMToolStripMenuItem.Checked = !markMToolStripMenuItem.Checked;
        }

        private void audioMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            audioMToolStripMenuItem.Checked = !audioMToolStripMenuItem.Checked;
        }

        private void settingSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Setting Setting = new Form_Setting(this);
            Setting.ShowDialog();
        }

        private void rankRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Rank Rank = new Form_Rank();
            Rank.ShowDialog();
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {

        }

        bool[,] pHasMine;
        enum State
        {
            None = 0, Open, Flag, Doubt
        }
        State[,] pState;

        private void UpdateState(ref State s)
        {
            switch(s)
            {
                case State.Open:
                    s = State.Open;
                    break;
                case State.None:
                    s = State.Flag;
                    Label_Mine.Text = Convert.ToString(Convert.ToInt32(Label_Mine.Text) - 1);
                    break;
                case State.Flag:
                    s = State.Doubt;
                    Label_Mine.Text = Convert.ToString(Convert.ToInt32(Label_Mine.Text) + 1);
                    break;
                case State.Doubt:
                    s = State.None;
                    break;
            }
        }

        private void BeginNewGame()
        {
            pHasMine = new bool[nWidth, nHeight];
            pState = new State[nWidth, nHeight];

            Array.Clear(pHasMine, 0, pHasMine.Length);
            Array.Clear(pState, 0, pState.Length);

            Random Rand = new Random();
            for (int i = 0; i < nMineCnt; i++)
            {
                int x = Rand.Next(nWidth);
                int y = Rand.Next(nHeight);
                if (pHasMine[x, y] == true)
                {
                    i--;
                    continue;
                }
                pHasMine[x, y] = true;
            }
            MouseFocus.X = MouseFocus.Y = -1;
            Label_Mine.Text = nMineCnt.ToString();
            Label_Timer.Text = "0";
            Timer_Main.Enabled = true;
            bGame_Running = true;
        }

        bool bGame_Running;

        private void newGameNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BeginNewGame();
        }

        Point MouseFocus;

        private void Form_Main_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X < 6 || e.X > 6 + nWidth*34 ||
                e.Y < 6 + MenuStrip_Main.Height ||
                e.Y > 6 + MenuStrip_Main.Height + nHeight*34)
            {
                MouseFocus.X = MouseFocus.Y = -1;
            }
            int x = (e.X - 6) / 34;
            int y = (e.Y - MenuStrip_Main.Height - 6) / 34;
            if (MouseFocus.X == x && MouseFocus.Y == y)
                return;
            MouseFocus.X = x;
            MouseFocus.Y = y;
            this.Refresh();
        }

        private void Timer_Main_Tick(object sender, EventArgs e)
        {
            if (audioMToolStripMenuItem.Checked)
            {
                soundTick.Play();
            }
            Label_Timer.Text = Convert.ToString(Convert.ToInt32(Label_Timer.Text) + 1);
        }

        bool bMouseLeft;
        bool bMouseRight;

        private void Form_Main_MouseLeave(object sender, EventArgs e)
        {
            MouseFocus.X = MouseFocus.Y = -1;
            this.Refresh();
        }

        private void Form_Main_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                bMouseLeft = true;
            }
            else if(e.Button == MouseButtons.Right)
            {
                bMouseRight = true;
            }
        }

        private void Form_Main_MouseUp(object sender, MouseEventArgs e)
        {
            if (MouseFocus.X == -1 && MouseFocus.Y == -1 || !bGame_Running)
            { return; }
            if(bMouseLeft && bMouseRight)
            {
                if (pState[MouseFocus.X, MouseFocus.Y] == State.Open)
                {
                    int nSysCnt = Around(pHasMine, MouseFocus.X, MouseFocus.Y).Count(x => x);
                    int nFlagCnt = Around(pState, MouseFocus.X, MouseFocus.Y).Count(state => state == State.Flag);
                    int nDoubtCnt = Around(pState, MouseFocus.X, MouseFocus.Y).Count(state => state == State.Doubt);
                    if (nFlagCnt == nSysCnt || nFlagCnt + nDoubtCnt == nSysCnt)
                    {
                        if (!OpenMine(MouseFocus.X, MouseFocus.Y))
                        {
                            GameEndWithLost();
                        }
                    }
                }
            }
            else if(bMouseLeft)
            {
                if (pState[MouseFocus.X, MouseFocus.Y] != State.Flag)
                {
                    if (pHasMine[MouseFocus.X, MouseFocus.Y] != true)
                    {
                        dfs(MouseFocus.X, MouseFocus.Y);
                    }
                    else
                    {
                        GameEndWithLost();
                    }
                }
            }
            else if(bMouseRight && markMToolStripMenuItem.Checked)
            {
                UpdateState(ref pState[MouseFocus.X, MouseFocus.Y]);
            }
            this.Refresh();
            GameEndWithWin();
            bMouseLeft = bMouseRight = false;
        }

        private bool OpenMine(int sx, int sy)
        {
            for (int i = 0; i < AroundCnt; i++)
            {
                int x = sx + dx[i];
                int y = sy + dy[i];
                if (pState[x, y] == State.None)
                {
                    pState[x, y] = State.Open;
                    if (pHasMine[x, y] == true)
                    {
                        return false;
                    }
                    else
                    {
                        dfs(x, y);
                    }
                }
            }
            return true;
        }

        const int AroundCnt = 8;
        static int[] dx = new int[AroundCnt] { -1, 0, 1, -1, 1, -1, 0, 1 };
        static int[] dy = new int[AroundCnt] { 1, 1, 1, 0, 0, -1, -1, -1 };

        private IEnumerable<T> Around<T>(T[,] r, int x, int y)
        {
            T t;
            for (int i = 0; i < AroundCnt; i++)
            {
                try
                {
                     t = r[x + dx[i], y + dy[i]];
                }
                catch(IndexOutOfRangeException)
                {
                    continue;
                }
                yield return t;
            }
        }

        private void dfs(int sx, int sy)
        {
            pState[sx, sy] = State.Open;
            int MineAround = Around(pHasMine, sx, sy).Count(x => x);
            // int QAround = Around(pState, sx, sy).Count(state => state == State.Q);
            for (int i = 0; i < AroundCnt; i++)
            {
                int x = sx + dx[i];
                int y = sy + dy[i];
                try
                {
                    if((pState[x, y] == State.None || pState[x, y] == State.Doubt) && !pHasMine[x, y])
                    {
                        if (Around(pHasMine, x, y).Count(k => k) == 0)
                            dfs(x, y);
                        else
                            pState[x, y] = State.Open;
                    }
                }
                catch(IndexOutOfRangeException)
                {
                    continue;
                }
            }
        }

        private void GameEndWithLost()
        {
            for (int i = 0; i < nWidth; i++)
            {
                for (int j = 0; j < nHeight; j++)
                {
                    if (pHasMine[i, j] && (pState[i, j] == State.None || pState[i, j] == State.Doubt))
                    {
                        pState[i, j] = State.Open;
                    }
                }
            }
            Timer_Main.Enabled = false;
            bGame_Running = false;
        }

        private void GameEndWithWin()
        {
            int notOpenCnt = 0;
            foreach (State state in pState)
            {
                if (state != State.Open)
                {
                    notOpenCnt++;
                }
            }
            if (notOpenCnt == nMineCnt)
            {
                Timer_Main.Enabled = false;
                MessageBox.Show(String.Format("YOU WIN! cost {0} second.", Label_Timer.Text), "MessageBox", MessageBoxButtons.OK);
                int x = (int)Math.Floor(10000.0 / Convert.ToDouble(Label_Timer.Text)); 
                switch(level)
                {
                    case Level.Beginner:
                        Properties.Settings.Default.Beginner = Math.Max(Properties.Settings.Default.Beginner, x);
                        break;
                    case Level.Intermediate:
                        Properties.Settings.Default.Intermediate = Math.Max(Properties.Settings.Default.Intermediate, x);
                        break;
                    case Level.Expert:
                        Properties.Settings.Default.Expert = Math.Max(Properties.Settings.Default.Expert, x);
                        break;
                }
                Properties.Settings.Default.Save();
                bGame_Running = false;
            }
        }


    }

    
}
