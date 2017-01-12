using System;
using System.Drawing;

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
        bool GameIsRunning;
        
        internal Size MineAreaSize
        {
            get
            {
                return new Size();
            }
        }

    }
}