namespace Miscd.Raft
{
    public readonly struct LogEntry
    {
        // TODO - representation of command for state machine

        // term when entry was received by leader
        public Term TermReceived { get;  }
    }
}
