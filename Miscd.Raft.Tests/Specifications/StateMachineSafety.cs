using Microsoft.Coyote;
using Microsoft.Coyote.Specifications;
using Miscd.Raft.Events;
using Miscd.Raft.Events.DiagnosticEvents;
using System.Linq;

namespace Miscd.Raft.Tests.Specifications
{
    // TODO - common inheritance with other monitors?
    // If a server has applied a log entry at a given index to its state machine, no other server will ever apply a different log entry for the same index
    public class StateMachineSafety : Monitor
    {
        private RaftClusterLogs ClusterLogs { get; } = new RaftClusterLogs();

        [Start]
        [OnEventDoAction(typeof(LogEntryAppliedEvent), nameof(RecordLogEntryApplication))]
        [IgnoreEvents(
            typeof(AppendEntriesRequestEvent),
            typeof(AppendEntriesResponseEvent),
            typeof(RequestFromClientEvent),
            typeof(RespondToClientEvent),
            typeof(VoteRequestEvent),
            typeof(VoteResponseEvent),
            typeof(LeaderElectedEvent),
            typeof(LogOverwrittenEvent)
        )]
        private class Monitoring : State
        {
        }

        private void RecordLogEntryApplication(Event e)
        {
            var logEntryApplication = e as LogEntryAppliedEvent;
            ClusterLogs.AppendToLog(logEntryApplication.ServerId, logEntryApplication.Entry);

            // check property
            var index = new LogIndex(ClusterLogs.Logs[logEntryApplication.ServerId].Count - 1);
            var otherServers = ClusterLogs.Logs.Keys
                .Where(serverId => logEntryApplication.ServerId != serverId);
            foreach (var otherServer in otherServers)
            {
                if (ClusterLogs.Logs[otherServer].Count >= index.Value)
                {
                   Assert(ClusterLogs.Logs[otherServer].ElementAt(index.Value).Equals(logEntryApplication.Entry));
                }
            }

        }
    }
}
