using Microsoft.Coyote;

namespace Miscd.Raft.Events
{
    public class VoteResponseEvent : Event
    {
        public Term Term { get; } // current term, for candidate to update itself
        public bool IsVoteGranted { get; } // true means candidate received vote (from responding server)
        public RaftServerId RespondingServer { get; }   // server that request was sent to (not in Raft paper; consequence of handling RPCs asynchronously)

        public VoteResponseEvent(Term term, bool isVoteGranted, RaftServerId respondingServer)
        {
            Term = term;
            IsVoteGranted = isVoteGranted;
            RespondingServer = respondingServer;
        }
    }
}
