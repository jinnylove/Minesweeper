namespace Minesweeper
{
    internal enum State
    {
        None = 0, Open, Flag, Doubt
    }
    static class StateUtil
    {
        static State NextState(this State s)
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
}
