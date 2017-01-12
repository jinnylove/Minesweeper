using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    internal enum Level
    {
        Beginner, Intermediate, Expert, Setting
    }
    static class LevelUtil
    {
        internal static void SetLevel(this MineArea mineArea, Level NewLevel)
        {
            switch (NewLevel)
            {
                case Level.Beginner:
                    mineArea.Set(10, 10, 10);
                    break;
                case Level.Intermediate:
                    mineArea.Set(16, 16, 40);
                    break;
                case Level.Expert:
                    mineArea.Set(30, 16, 99);
                    break;
            }
        }
        internal static Level GetLevel(this MineArea mineArea)
        {
            if (mineArea.Is(10, 10, 10))
            {
                return Level.Beginner;
            }
            else if (mineArea.Is(16, 16, 40))
            {
                return Level.Intermediate;
            }
            else if (mineArea.Is(30, 16, 99))
            {
                return Level.Expert;
            }
            return Level.Setting;
        }
    }
}
