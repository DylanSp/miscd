using Microsoft.Coyote;

namespace Miscd.Raft.Events
{
    public class VoteResponseEvent : Event
    {
        public Term Term { get; } // current term, for candidate to update itself
        public bool IsVoteGranted { get; } // true means candidate received vote (from responding server)

        public VoteResponseEvent(Term term, bool isVoteGranted)
        {
            Term = term;
            IsVoteGranted = isVoteGranted;
        }
    }
}
