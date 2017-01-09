using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace Minesweeper
{
    internal enum Level
    {
        Beginner, Intermediate, Expert, Setting
    }
    internal class Game
    {
        MineAreaDrawable mineArea;
        SoundPlayer TickSound;

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
            throw new NotImplementedException();
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

            mineArea.clear();

            Label_Mine.Text = mineArea.MineCount.ToString();
            Label_Timer.Text = "0";
            Timer_Main.Enabled = true;
            GameIsRunning = true;
        }

        internal void UpdateMarkable(bool @checked)
        {
            throw new NotImplementedException();
        }

        internal void SetAudioable(bool @checked)
        {
            throw new NotImplementedException();
        }

        internal void UpdateAudioable(bool @checked)
        {
            throw new NotImplementedException();
        }

        internal void SetMarkable(bool @checked)
        {
            throw new NotImplementedException();
        }

        private void End()
        {
            Timer_Main.Enabled = false;
            GameIsRunning = false;
        }
        private void CheckWin()
        {
            return;

            // GameEnd();
            // ShowWinMessageBox();
            // UpdateRank();

        }
        private void ShowWinMessageBox()
        {
            MessageBox.Show(String.Format("YOU WIN! cost {0} second.", Label_Timer.Text), "MessageBox", MessageBoxButtons.OK);
        }

        internal void OnClockTick()
        {
            if (audioMToolStripMenuItem.Checked)
            {
                TickSound.Play();
            }
            Label_Timer.Text = Convert.ToString(Convert.ToInt32(Label_Timer.Text) + 1);
            throw new NotImplementedException();
        }

        private void UpdateRank()
        {
            int x = (int)Math.Floor(10000.0 / Convert.ToDouble(Form_Main.Label_Timer.Text));
            switch (game.level)
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
        }
    }
}