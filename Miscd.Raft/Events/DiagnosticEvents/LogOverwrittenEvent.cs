using Microsoft.Coyote;

namespace Miscd.Raft.Events.DiagnosticEvents
{
    public class LogOverwrittenEvent : Event
    {
        public Term Term { get; }
        public RaftServerId OverwritingServerId { get; }

        public LogOverwrittenEvent(Term term, RaftServerId overwritingServerId)
        {
            Term = term;
            OverwritingServerId = overwritingServerId;
        }
    }
}
