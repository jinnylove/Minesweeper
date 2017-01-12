using System;

namespace Minesweeper
{
    class Rank
    {

    }
    static class RankUtil
    {
        internal static void Update(this Rank rank, Level level, int time)
        {
            int x = (int)Math.Floor(10000.0 / time);
            switch (level)
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
