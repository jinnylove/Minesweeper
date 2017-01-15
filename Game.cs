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

    public abstract class AbstractNode
    {
        public State state = State.None;

        public AbstractNode(AbstractNode[,] p, int row, int colume)
        {
            parentBox = new WeakReference<AbstractNode[,]>(p);
            location = new Point(row, colume);
        }

        private WeakReference<AbstractNode[,]> parentBox;
        public AbstractNode[,] parent
        {
            get
            {
                AbstractNode[,] p;
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

        public PointF center
        {
            get
            {
                return new PointF(figure.Sum(p => p.X)/figure.Length, figure.Sum(p=>p.Y)/figure.Length);
            }
        }

        public AbstractNode[] around
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
        public abstract bool right { get; }

        public static AbstractNode nwFind(AbstractNode[,] matrix, PointF site)
        {
            int rowMin = 0, columeMin = 0;
            PointF c0 = matrix[0, 0].center;
            PointF cc = matrix[0, 1].center;
            PointF cr = matrix[1, 0].center;
            float dxc = cc.X - c0.X;
            float dyc = cc.Y - c0.Y;
            float dxr = cr.X - c0.X;
            float dyr = cr.Y - c0.Y;
            float dxs = site.X - c0.X;
            float dys = site.Y - c0.Y;
            float det = dxr * dyc - dyr * dxc;
            float rowf = (dxs * dyc - dys * dxc) / det;
            float columef = (dxr * dys - dyr * dxs) / det;
            float distance2Min = float.MaxValue;
            for (int row = (int)Math.Floor(rowf); row <= Math.Ceiling(rowf); row++)
            {
                for (int colume = (int)Math.Floor(columef); colume <= Math.Ceiling(columef); colume++)
                {
                    float distance2 = (matrix[row, colume].center.X - site.X).pow2() + (matrix[row, colume].center.Y - site.Y).pow2();
                    if (distance2 < distance2Min)
                    {
                        distance2Min = distance2;
                        rowMin = row;
                        columeMin = colume;
                    }
                }
            }
            return matrix[rowMin, columeMin];
        }
        
        public static PointF nwGet(AbstractNode[,] arr)
        {
            float minX = float.MaxValue, minY = float.MaxValue, maxX = float.MinValue, maxY = float.MinValue;
            foreach (AbstractNode node in arr)
            {
                foreach (PointF point in node.figure)
                {
                    if (point.X < minX)
                    {
                        minX = point.X;
                    }
                    if (point.Y < minY)
                    {
                        minY = point.Y;
                    }
                    if (point.X > maxX)
                    {
                        maxX = point.X;
                    }
                    if (point.Y > maxY)
                    {
                        maxY = point.Y;
                    }
                }
            }
            return new PointF(maxX - minX, maxY - minY);
        }

        internal void OnPaint(Graphics g)
        {
            if (state == State.None)
            {
                g.FillPolygon(Brushes.SandyBrown, figure);
            }

            //SizeF Size = g.MeasureString(mineAreaNode.AroundMineCount.ToString(), font);
            //g.DrawString(mineAreaNode.AroundMineCount.ToString(), font, brushs[mineAreaNode.AroundMineCount], mineAreaNode.g);

        }

        public void Open()
        {
            state = State.Open;
        }

    }
    class RectangleNode : AbstractNode
    {
        public override PointF[] figure
        {
            get
            {
                var x = (location.Y - 1) * WIDTH * (1 + SUBWIDTH);
                var y = (location.X - 1) * WIDTH * (1 + SUBWIDTH);
                var a = from p in DPoint
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

        private static PointF[] DPoint = new PointF[]
        {
            new PointF(0F, 0F),
            new PointF(0F, WIDTH),
            new PointF(WIDTH, WIDTH),
            new PointF(WIDTH, 0F)
        };

        public RectangleNode(AbstractNode[,] p, int row, int colume) : base(p, row, colume) { }

        public override Point[] aroundLocation { get { return AroundLocation; } }

        public override bool right
        {
            get
            {
                return location.X != 0 && location.Y != 0 && location.X != parent.GetLength(1) && location.Y != parent.GetLength(0);
            }
        }
    }
    class SixNode : AbstractNode
    {
        public override Point[] aroundLocation
        {
            get
            {
                return AroundLocation;
            }
        }

        private static Point[] AroundLocation = new Point[]
        {
            new Point(-1, -1),
            new Point(-1, 0),
            new Point(0, -1),
            new Point(0, 1),
            new Point(1, 0),
            new Point(1, 1)
        };

        static PointF[] DPoint = new PointF[]
        {
            new PointF(0F, 0F),
            new PointF((float)Math.Sqrt(3) / 2F * WIDTH, WIDTH / 2F),
            new PointF((float)Math.Sqrt(3) / 2F * WIDTH, WIDTH / 2F * 3F),
            new PointF(0F, WIDTH* 2F),
            new PointF(-(float)Math.Sqrt(3) / 2F * WIDTH, WIDTH / 2F * 3F),
            new PointF(-(float)Math.Sqrt(3) / 2F * WIDTH, WIDTH / 2F)
        };

        public SixNode(AbstractNode[,] p, int row, int colume) : base(p, row, colume)
        {
        }

        public override PointF[] figure
        {
            get
            {
                var x = (location.Y - location.X / 2F - 1 / 2F) * WIDTH * ((float)Math.Sqrt(3) * (1 + SUBWIDTH)) + (float)Math.Sqrt(3) / 2F * WIDTH;
                var y = (location.X - 1) * WIDTH * (1.5F + SUBWIDTH);

                var a = from p in DPoint
                        select new PointF(p.X + x + OFFSET.X, p.Y + y + OFFSET.Y);

                return a.ToArray();
            }
        }

        public override bool right
        {
            get
            {
                if (location.X != 0 && location.Y != 0 && location.X != parent.GetLength(1) && location.Y != parent.GetLength(0))
                {
                    int row = parent.GetLength(0) - 2;
                    int colume = parent.GetLength(1) - 2;
                    return location.Y >= location.X / 2 + 1 && location.Y <= (colume - row / 2) + location.X / 2;
                }
                return false;
            }
        }
    }


    public class Game
    {
        public AbstractNode[,] mineArea;
        public Queue<AbstractNode> updateQueue = new Queue<AbstractNode>();

        public Game() : this(10, 10, 10) { }

        public Game(int rowCnt, int columeCnt, int mineCnt)
        {
            mineArea = new AbstractNode[rowCnt + 2, columeCnt + 2];
            foreach (int i in Enumerable.Range(0, mineArea.GetLength(0) - 1))
            {
                foreach (int j in Enumerable.Range(0, mineArea.GetLength(1) - 1))
                {
                    mineArea[i, j] = new SixNode(mineArea, i, j);
                    if (mineArea[i, j].right)
                    {
                        updateQueue.Enqueue(mineArea[i, j]);
                    }
                }
            }

            Random random = new Random();
            while (mineCnt != 0)
            {
                var row = random.Next(rowCnt) + 1;
                var colume = random.Next(columeCnt) + 1;
                if (mineArea[row, colume].hasMine)
                    continue;
                mineArea[row, colume].hasMine = true;
                mineCnt--;
            }
        }

        

        internal void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

            }
        }
        public void OnPaint(Graphics g)
        {
            foreach (var n in updateQueue)
            {
                n.OnPaint(g);
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
        public static  float pow2(this float d)
        {
            return d * d;
        }
    }

}
