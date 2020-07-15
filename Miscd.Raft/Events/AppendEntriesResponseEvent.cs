using Microsoft.Coyote;

namespace Miscd.Raft.Events
{
    public class AppendEntriesResponseEvent : Event
    {
        public Term Term { get; } // currentTerm, for leader to update itself
        public bool IsSuccess { get; } // true if follower contained entry matching PrevLogIndex and PrevLogTerm (from request)
    }
}
