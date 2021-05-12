namespace Miscd.Raft.RPCTypes
{
    public class AppendEntriesResponse
    {
        public Term Term { get; } // currentTerm, for leader to update itself
        public bool IsSuccess { get; } // true if follower contained entry matching PrevLogIndex and PrevLogTerm (from request)

        public AppendEntriesResponse(Term term, bool isSuccess)
        {
            Term = term;
            IsSuccess = isSuccess;
        }
    }
}
