using Microsoft.Coyote;
using Microsoft.Coyote.Specifications;
using Miscd.Raft.Events;
using Miscd.Raft.Events.DiagnosticEvents;

namespace Miscd.Raft.Tests.Specifications
{
    // If a log entry is committed in a given term, then that entry will be present in the logs of the leaders for all higher-numbered terms.
    public class LeaderCompleteness : Monitor
    {
        [Start]
        [OnEventDoAction(typeof(LeaderElectedEvent), nameof(RecordLeaderElection))]
        [OnEventDoAction(typeof(LogEntryAppliedEvent), nameof(RecordLogEntryApplication))]
        [IgnoreEvents(
            typeof(AppendEntriesRequestEvent),
            typeof(AppendEntriesResponseEvent),
            typeof(RequestFromClientEvent),
            typeof(VoteRequestEvent),
            typeof(VoteResponseEvent)
        )]
        private class Monitoring : State
        {
        }

        private void RecordLeaderElection(Event e)
        {
            var election = e as LeaderElectedEvent;
        }

        private void RecordLogEntryApplication(Event e)
        {
            var logEntryApplication = e as LogEntryAppliedEvent;
        }
    }
}
