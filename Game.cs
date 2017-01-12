using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    public enum State
    {
        None = 0, Open, Flag, Doubt
    }
    static class StateUtil
    {
        internal static State NextState(this State s)
        {
            switch (s)
            {
                case State.Open:
                    return State.Open;
                case State.None:
                    return State.Flag;
                case State.Flag:
                    return State.Doubt;
                case State.Doubt:
                    return State.None;
            }
            return State.None;
        }
    }
    static class PaintUtil
    {
        static Font font = new Font("Consolas", 16);
        static Brush[] brushs = new Brush[]
        {
                    null,
                    new SolidBrush(Color.Blue),
                    new SolidBrush(Color.Green),
                    new SolidBrush(Color.Red),
                    new SolidBrush(Color.DarkBlue),
                    new SolidBrush(Color.DarkRed),
                    new SolidBrush(Color.DarkSeaGreen),
                    new SolidBrush(Color.Black),
                    new SolidBrush(Color.DarkGray)
        };
    }
    public class Node
    {
        public State state;
        public bool hasMine;
        public int mineCountAround;
        public PointF[] figure;
    }
    public class Nodes
    {
        public Node[,] nodes;
        public Nodes(int rowCnt, int columeCnt, int mineCnt)
        {
            nodes = new Node[rowCnt, columeCnt];
            
            foreach (int i in Enumerable.Range(0, rowCnt))
            {
                foreach (int j in Enumerable.Range(0, columeCnt))
                {
                    nodes[i, j] = new Node();
                    Node n = nodes[i, j];

                    n.state = State.None;
                    n.hasMine = false;
                    n.mineCountAround = 0;
                    n.figure = getFigure(i, j);
                }
            }

            Random random = new Random();
            while(mineCnt != 0)
            {
                var row = random.Next(rowCnt);
                var colume = random.Next(columeCnt);
                if (nodes[row, colume].hasMine)
                    continue;
                nodes[row, colume].hasMine = true;
                mineCnt--;
            }
        }
        public PointF[] getFigure(int i, int j)
        {
            return getSix(i, j);
        }

        static float width = 30F;
        static float subWidth = 1F / 12;
        static PointF[] dRect = new PointF[]
        {
            new PointF(0F, 0F),
            new PointF(0F, width),
            new PointF(width, width),
            new PointF(width, 0F)
        };
        static PointF[] dSix = new PointF[]
        {
            new PointF(0F, 0F),
            new PointF((float)Math.Sqrt(3) / 2F * width, width / 2F),
            new PointF((float)Math.Sqrt(3) / 2F * width, width / 2F * 3F),
            new PointF(0F, width* 2F),
            new PointF(-(float)Math.Sqrt(3) / 2F * width, width / 2F * 3F),
            new PointF(-(float)Math.Sqrt(3) / 2F * width, width / 2F)
        };
        public PointF[] getRect(int row, int colume)
        {     
            var x = row * width * (1 + subWidth);
            var y = colume * width * (1 + subWidth);
            
            return dRect.Select((p) => new PointF(p.X + x, p.Y + y)).ToArray();
        }
        
        public PointF[] getSix(int row, int colume)
        {
            var x = (row - colume / 2F) * width * ((float)Math.Sqrt(3) * (1 + subWidth)) + (float)Math.Sqrt(3) / 2F * width;
            var y = colume * width * (1.5F + subWidth);

            return dSix.Select((p) => new PointF(p.X + x, p.Y + y)).ToArray();
        }
    }
    public class Game
    {
        public Nodes mineArea;

        public Game(int rowCnt, int columeCnt, int mineCnt)
        {
            mineArea = new Minesweeper.Nodes(rowCnt, columeCnt, mineCnt);
        }
        public void PaintTo(PaintEventArgs e)
        {
            foreach (Node n in mineArea.nodes)
            {
                e.Graphics.FillPolygon(Brushes.SandyBrown, n.figure);
                //SizeF Size = g.MeasureString(mineAreaNode.AroundMineCount.ToString(), font);
                //g.DrawString(mineAreaNode.AroundMineCount.ToString(), font, brushs[mineAreaNode.AroundMineCount], mineAreaNode.g);

            }
        }
    }
}