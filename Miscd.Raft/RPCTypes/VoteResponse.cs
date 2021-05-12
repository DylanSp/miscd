namespace Miscd.Raft.RPCTypes
{
    public class VoteResponse
    {
        public Term Term { get; } // current term, for candidate to update itself
        public bool IsVoteGranted { get; } // true means candidate received vote (from responding server)

        public VoteResponse(Term term, bool isVoteGranted)
        {
            Term = term;
            IsVoteGranted = isVoteGranted;
        }
    }
}
