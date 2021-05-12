using Microsoft.Coyote;
using System.Collections.Generic;

namespace Miscd.Raft.Events
{
    // raised by Raft Controller when AppendEntries HTTP call is received
    public class ReceiveAppendEntriesRequestEvent : Event
    {
        public Term Term { get; } // leader's term
        public RaftServerId LeaderId { get; } // so follower can redirect clients
        public LogIndex PrevLogIndex { get; } // index of log entry immediately preceding new ones
        public Term PrevLogTerm { get; } // term of PrevLogIndex entry
        public List<LogEntry> Entries { get; } // log entries to store (empty for heartbeat, may send more than one for efficiency)
        public LogIndex LeaderCommitIndex { get; } // leader's CommitIndex

        public ReceiveAppendEntriesRequestEvent(Term term, RaftServerId leaderId, LogIndex prevLogIndex, Term prevLogTerm, List<LogEntry> entries, LogIndex leaderCommitIndex)
        {
            Term = term;
            LeaderId = leaderId;
            PrevLogIndex = prevLogIndex;
            PrevLogTerm = prevLogTerm;
            Entries = entries;
            LeaderCommitIndex = leaderCommitIndex;
        }
    }
}
