using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace Minesweeper
{
    internal class MineAreaNode
    {
        public State state;
        public bool HasMine;
        public int AroundMineCount;
        internal RectangleF g;
    }
    internal class MineArea
    {
        internal MineAreaNode[,] mineAreaNodes;
        internal MineArea(int RowCount, int ColumeCount, int MineCount)
        {
            mineAreaNodes = new MineAreaNode[RowCount, ColumeCount];
            
            foreach(MineAreaNode n in mineAreaNodes)
            {
                n.state = State.None;
                n.HasMine = false;
                n.AroundMineCount = 0;
            }
            Random random = new Random();
            while(MineCount != 0)
            {
                int row = random.Next(RowCount);
                int colume = random.Next(ColumeCount);
                if (mineAreaNodes[row, colume].HasMine)
                    continue;
                mineAreaNodes[row, colume].HasMine = true;
                MineCount--;
            }
        }
    }
    internal class Game
    {
        MineArea mineArea;
        SoundPlayer TickSound;
        Rank rank;
        Clock clock;
        MineC mineC;
        internal Level level;

        bool Audioable;
        bool Markable;
        //SoundPlayer soundBomb;
        bool GameIsRunning;
        
        internal Size MineAreaSize
        {
            get
            {
                return new Size();
            }
        }


        internal Game()
        {
            GameIsRunning = false;
        }

        internal void SetLevel(int mineAreaRowCount, int mineAreaColumeCount, int mineAreaMineCount)
        {
            mineArea = new MineArea(mineAreaRowCount, mineAreaColumeCount, mineAreaMineCount);
            mineC.MineCount = mineAreaMineCount;
        }

        internal void SetTickClockSound(UnmanagedMemoryStream tickingClockSound)
        {
            TickSound = new SoundPlayer(tickingClockSound);
        }
        
        internal void SetClock(Clock c)
        {
            clock = c;
        }

        internal void SetMineC(MineC x)
        {
            mineC = x;
        }

        internal void BeginNewGame()
        {
            mineC.MineCount = 100;
            clock.ReStart();
            GameIsRunning = true;
        }

        
        internal void SetAudioable(bool @checked)
        {
            Audioable = @checked;
        }

        internal void UpdateAudioable(bool @checked)
        {
            Audioable = @checked;
        }

        internal void SetMarkable(bool @checked)
        {
            Markable = @checked;
        }
        internal void UpdateMarkable(bool @checked)
        {
            Markable = @checked;
        }

        private void Stop()
        {
            clock.Stop();
            GameIsRunning = false;
        }

        private void ShowWinMessageBox()
        {
            MessageBox.Show(String.Format("YOU WIN! cost {0} second.", clock.Time), "MessageBox", MessageBoxButtons.OK);
        }

        internal void OnClockTick()
        {
            if (Audioable)
            {
                TickSound.Play();
            }
            clock.Time++;
        }
    }
}