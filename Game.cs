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
    }
    internal class MineArea
    {

        internal int RowCount;
        internal int ColumeCount;
        internal int MineCount;
        internal MineArea() { }
        internal MineArea(int RowCount, int ColumeCount, int MineCount)
        {
            Set(RowCount, ColumeCount, MineCount);
        }

        internal void Set(int RowCount, int ColumeCount, int MineCount)
        {
            this.RowCount = RowCount;
            this.ColumeCount = ColumeCount;
            this.MineCount = MineCount;
        }

        internal bool Is(int RowCount, int ColumeCount, int MineCount)
        {
            return this.RowCount == RowCount
                && this.ColumeCount == ColumeCount
                && this.MineCount == MineCount;
        }
    }
    internal class Game
    {
        MineArea mineArea;
        SoundPlayer TickSound;
        Rank rank;
        Clock clock;
        MineC mineC;

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

        internal void SetLevel(int mineAreaRowCount, int mineAreaColumeCount, int mineAreaMineCnt)
        {
            mineArea = new MineArea(RowCount, ColumeCount, MineCount);
            throw new NotImplementedException();
        }

        internal void SetTickClockSound(UnmanagedMemoryStream tickingClockSound)
        {
            TickSound = new SoundPlayer(tickingClockSound);
        }

        internal Level GetLevel()
        {
            return mineArea.GetLevel();
        }
        
        internal void SetClock(Clock c)
        {
            clock = c;
        }

        internal void SetMineC(MineC x)
        {
            mineC = x;
        }

        internal void SetLevel(Level beginner)
        {
            throw new NotImplementedException();
        }

        internal void PaintTo(PaintEventArgs e)
        {
            throw new NotImplementedException();
        }

        internal void BeginNewGame()
        {
            mineC.MineCount = mineArea.MineCount;
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