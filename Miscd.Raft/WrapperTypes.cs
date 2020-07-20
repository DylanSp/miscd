using System;

namespace Miscd.Raft
{
    public readonly struct Term
    {
        public int Value { get; }

        public Term(int value)
        {
            Value = value;
        }

        public static bool operator ==(Term term1, Term term2)
        {
            return term1.Equals(term2);
        }

        public static bool operator !=(Term term1, Term term2)
        {
            return !term1.Equals(term2);
        }

        public override bool Equals(object? obj)
        {
            return obj is Term t &&
                t.Value == Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
    }

    public readonly struct LogIndex
    {
        public int Value { get; }

        public LogIndex(int value)
        {
            Value = value;
        }

        public static bool operator ==(LogIndex index1, LogIndex index2)
        {
            return index1.Equals(index2);
        }

        public static bool operator !=(LogIndex index1, LogIndex index2)
        {
            return !index1.Equals(index2);
        }

        public override bool Equals(object? obj)
        {
            return obj is LogIndex li &&
                li.Value == Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
    }

    public readonly struct RaftServerId
    {
        public string Value { get; }

        public RaftServerId(string value)
        {
            Value = value;
        }

        public static bool operator ==(RaftServerId serverId1, RaftServerId serverId2)
        {
            return serverId1.Equals(serverId2);
        }

        public static bool operator !=(RaftServerId serverId1, RaftServerId serverId2)
        {
            return !serverId1.Equals(serverId2);
        }

        public override bool Equals(object? obj)
        {
            return obj is RaftServerId rsId &&
                rsId.Value == Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value);
        }
    }
}
