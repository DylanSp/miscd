namespace Miscd.Raft.RPCTypes
{
    public class VoteRequest
    {
        public Term Term { get; }    // candidate's term
        public RaftServerId CandidateId { get; } // candidate requesting vote
        public LogIndex LastLogIndex { get; } // index of candidate's last log entry
        public Term LastLogTerm { get; } // term of candidate's last log entry

        public VoteRequest(Term term, RaftServerId candidateId, LogIndex lastLogIndex, Term lastLogTerm)
        {
            Term = term;
            CandidateId = candidateId;
            LastLogIndex = lastLogIndex;
            LastLogTerm = lastLogTerm;
        }
    }
}
