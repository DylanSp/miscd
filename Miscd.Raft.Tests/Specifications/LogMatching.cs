using Microsoft.Coyote;
using Microsoft.Coyote.Specifications;
using Miscd.Raft.Events;
using Miscd.Raft.Events.DiagnosticEvents;
using System.Collections.Generic;
using System.Linq;

namespace Miscd.Raft.Tests.Specifications
{
    // TODO - many of the same properties, functions as LeaderCompleteness; some kind of inheritance?
    // If two logs contain an entry with the same index and term, then the logs are identical in all entries up through the given index
    public class LogMatching : Monitor
    {
        private RaftClusterLogs ClusterLogs { get; } = new RaftClusterLogs();

        private Dictionary<Term, RaftServerId> LeadersByTerm { get; } = new Dictionary<Term, RaftServerId>();

        [Start]
        [OnEventDoAction(typeof(LeaderElectedEvent), nameof(RecordLeaderElection))]
        [OnEventDoAction(typeof(LogEntryAppliedEvent), nameof(RecordLogEntryApplication))]
        [IgnoreEvents(
            typeof(AppendEntriesRequestEvent),
            typeof(AppendEntriesResponseEvent),
            typeof(RequestFromClientEvent),
            typeof(RespondToClientEvent),
            typeof(VoteRequestEvent),
            typeof(VoteResponseEvent),
            typeof(LogOverwrittenEvent)
        )]
        private class Monitoring : State
        {
        }

        private void RecordLeaderElection(Event e)
        {
            var election = e as LeaderElectedEvent;
            LeadersByTerm[election.Term] = election.LeaderId;
        }

        private void RecordLogEntryApplication(Event e)
        {
            var logEntryApplication = e as LogEntryAppliedEvent;
            ClusterLogs.AppendToLog(logEntryApplication.ServerId, logEntryApplication.Entry);

            // check property
            var otherServers = ClusterLogs.Logs.Keys
                .Where(serverId => logEntryApplication.ServerId != serverId);
            foreach (var otherServer in otherServers)
            {
                if (ClusterLogs.Logs[otherServer].Contains(logEntryApplication.Entry))
                {
                    for (var i = 0; i < ClusterLogs.Logs[otherServer].IndexOf(logEntryApplication.Entry); i++)
                    {
                        Assert(ClusterLogs.Logs[otherServer].ElementAt(i).Equals(ClusterLogs.Logs[logEntryApplication.ServerId].ElementAt(i)));
                    }
                }
            }
        }
    }
}
