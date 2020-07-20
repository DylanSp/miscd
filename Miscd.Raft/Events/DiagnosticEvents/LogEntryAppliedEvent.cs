using Microsoft.Coyote;

namespace Miscd.Raft.Events.DiagnosticEvents
{
    public class LogEntryAppliedEvent : Event
    {
        public LogEntry Entry { get; }

        public RaftServerId ServerId { get; }

        public LogEntryAppliedEvent(LogEntry entry, RaftServerId serverId)
        {
            Entry = entry;
            ServerId = serverId;
        }
    }
}
