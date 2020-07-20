using Microsoft.Coyote;

namespace Miscd.Raft.Events.DiagnosticEvents
{
    public class LeaderElectedEvent : Event
    {
        public Term Term { get; }
        public RaftServerId LeaderId { get; }

        public LeaderElectedEvent(Term term, RaftServerId leaderId)
        {
            Term = term;
            LeaderId = leaderId;
        }
    }
}
