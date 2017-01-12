using System;
using System.Windows.Forms;

namespace Minesweeper
{
    class Clock
    {
        Label TimeLabel;
        Timer timer;
        internal Clock(Timer t, Label label)
        {
            TimeLabel = label;
            timer = t;
        }
        internal int Time
        {
            get
            {
                return Convert.ToInt32(TimeLabel.Text);
            }
            set
            {
                TimeLabel.Text = value.ToString();
            }
        }
        internal void Reset()
        {
            Stop();
            Time = 0;
        }
        internal void Stop()
        {
            timer.Enabled = false;
        }
        internal void Start()
        {
            timer.Enabled = true;
        }
        internal void ReStart()
        {
            Reset();
            Start();
        }
    }
}
