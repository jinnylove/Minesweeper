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
        internal static void SetLevel(this Game game, Level NewLevel)
        {
            game.level = NewLevel;
            switch (NewLevel)
            {
                case Level.Beginner:
                    game.SetLevel(10, 10, 10);
                    break;
                case Level.Intermediate:
                    game.SetLevel(16, 16, 40);
                    break;
                case Level.Expert:
                    game.SetLevel(30, 16, 99);
                    break;
            }
        }
    }
}
