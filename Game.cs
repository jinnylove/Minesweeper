using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Minesweeper
{
    public enum State
    {
        None = 0, Open, Flag, Doubt
    }
    static class StateUtil
    {
        public static State NextState_Right(this State s)
        {
            switch (s)
            {
                case State.None:
                    return State.Flag;
                case State.Flag:
                    return State.Doubt;
                case State.Doubt:
                    return State.None;
            }
            throw new NotImplementedException();
        }
        public static State NextState_Left(this State s)
        {
            switch (s)
            {
                case State.None:
                    return State.Open;
            }
            throw new NotImplementedException();
        }

    }

    public abstract class Node
    {
        public State state = State.None;

        public Node(Node[,] p, int row, int colume)
        {
            parentBox = new WeakReference<Node[,]>(p);
            location = new Point(row, colume);
        }

        private WeakReference<Node[,]> parentBox;
        public Node[,] parent
        {
            get
            {
                Node[,] p;
                parentBox.TryGetTarget(out p);
                return p;
            }
        }
        public Point location;
        public bool hasMine = false;

        public int mineCountAround
        {
            get
            {
                return around.Count(x => x.hasMine);
            }
        }

        public abstract PointF[] figure { get; }

        public Node[] around
        {
            get
            {
                var a = from p in aroundLocation
                        select parent[p.X + location.X, p.Y + location.Y];
                return a.ToArray();
            }
        }

        public abstract Point[] aroundLocation { get; }

        public static float WIDTH = 30F;
        public static float SUBWIDTH = 1F / 12;
        public static PointF OFFSET = new PointF(2 * SUBWIDTH, 2 * SUBWIDTH);
        public bool right
        {
            get
            {
                return location.X != 0 && location.Y != 0 &&
                location.X != parent.GetLength(1) && location.Y != parent.GetLength(0);
            }
        }
    }
    class RectangleNode : Node
    {
        public override PointF[] figure
        {
            get
            {
                var x = (location.X - 1) * WIDTH * (1 + SUBWIDTH);
                var y = (location.Y - 1) * WIDTH * (1 + SUBWIDTH);
                var a = from p in dRect
                        select new PointF(p.X + x + OFFSET.X, p.Y + y + OFFSET.Y);

                return a.ToArray();
            }
        }

        private static Point[] AroundLocation = new Point[]
        {
            new Point(-1, -1),
            new Point(-1, 0),
            new Point(-1, 1),
            new Point(0, -1),
            new Point(0, 1),
            new Point(1, -1),
            new Point(1, 0),
            new Point(1, 1)
        };

        private static PointF[] dRect = new PointF[]
        {
            new PointF(0F, 0F),
            new PointF(0F, WIDTH),
            new PointF(WIDTH, WIDTH),
            new PointF(WIDTH, 0F)
        };

        public RectangleNode(Node[,] p, int row, int colume) : base(p, row, colume) { }

        public override Point[] aroundLocation { get { return AroundLocation; } }


    }
    class SixNode : Node
    {
        public override Point[] aroundLocation
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        static PointF[] dSix = new PointF[]
        {
            new PointF(0F, 0F),
            new PointF((float)Math.Sqrt(3) / 2F * WIDTH, WIDTH / 2F),
            new PointF((float)Math.Sqrt(3) / 2F * WIDTH, WIDTH / 2F * 3F),
            new PointF(0F, WIDTH* 2F),
            new PointF(-(float)Math.Sqrt(3) / 2F * WIDTH, WIDTH / 2F * 3F),
            new PointF(-(float)Math.Sqrt(3) / 2F * WIDTH, WIDTH / 2F)
        };

        public SixNode(Node[,] p, int row, int colume) : base(p, row, colume)
        {
        }

        public override PointF[] figure
        {
            get
            {
                var x = (location.X - location.Y / 2F - 1 / 2F) * WIDTH * ((float)Math.Sqrt(3) * (1 + SUBWIDTH)) + (float)Math.Sqrt(3) / 2F * WIDTH;
                var y = (location.Y - 1) * WIDTH * (1.5F + SUBWIDTH);

                var a = from p in dSix
                        select new PointF(p.X + x + OFFSET.X, p.Y + y + OFFSET.Y);

                return a.ToArray();
            }
        }
        public new bool right
        {
            get
            {
                if (base.right == false)
                    return false;

                int row = parent.GetLength(1) - 2;
                int colume = parent.GetLength(0) - 2;
                int y = location.X / 2 + 1;
                return location.Y >= location.X / 2 + 1;
            }
        }
    }


    public class Game
    {
        public Node[,] nodes;
        public Queue<Node> updateNodes = new Queue<Node>();

        public Game() : this(10, 10, 10) { }

        public Game(int rowCnt, int columeCnt, int mineCnt)
        {
            nodes = new Node[rowCnt + 2, columeCnt + 2];
            foreach (int i in Enumerable.Range(0, nodes.GetLength(1)))
            {
                foreach (int j in Enumerable.Range(0, nodes.GetLength(0)))
                {
                    nodes[i, j] = new SixNode(nodes, i, j);
                    if (nodes[i, j].right)
                    {
                        updateNodes.Enqueue(nodes[i, j]);
                    }
                }
            }

            Random random = new Random();
            while (mineCnt != 0)
            {
                var row = random.Next(rowCnt) + 1;
                var colume = random.Next(columeCnt) + 1;
                if (nodes[row, colume].hasMine)
                    continue;
                nodes[row, colume].hasMine = true;
                mineCnt--;
            }
        }

        public void Open(Node n)
        {
            n.state = State.Open;
        }

        internal void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

            }
        }
        public void PaintTo(Graphics g)
        {
            foreach (Node n in updateNodes)
            {
                if (n.state == State.None)
                {
                    g.FillPolygon(Brushes.SandyBrown, n.figure);
                }

                //SizeF Size = g.MeasureString(mineAreaNode.AroundMineCount.ToString(), font);
                //g.DrawString(mineAreaNode.AroundMineCount.ToString(), font, brushs[mineAreaNode.AroundMineCount], mineAreaNode.g);

            }
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

}