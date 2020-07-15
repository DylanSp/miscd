namespace Miscd.Raft
{
    public readonly struct Term
    {
        public int Value { get; }

        public Term(int value)
        {
            Value = value;
        }
    }

    public readonly struct LogIndex
    {
        public int Value { get; }

        public LogIndex(int value)
        {
            Value = value;
        }
    }

    public readonly struct RaftServerId
    {
        public string Value { get; }

        public RaftServerId(string value)
        {
            Value = value;
        }
    }
}
