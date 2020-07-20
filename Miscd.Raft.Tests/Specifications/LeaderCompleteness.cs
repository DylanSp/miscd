using Microsoft.Coyote;
using Microsoft.Coyote.Specifications;
using Miscd.Raft.Events;
using Miscd.Raft.Events.DiagnosticEvents;
using System.Collections.Generic;
using System.Linq;

namespace Miscd.Raft.Tests.Specifications
{
    // If a log entry is committed in a given term, then that entry will be present in the logs of the leaders for all higher-numbered terms.
    public class LeaderCompleteness : Monitor
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

            // check property
            foreach (var (serverId, entries) in ClusterLogs.Logs)
            {
                foreach (var entry in entries)
                {
                    var higherTermLeaders = LeadersByTerm
                        .Where(kvp => kvp.Key.Value > entry.TermReceived.Value)
                        .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                    var leaderOfTerm = higherTermLeaders[entry.TermReceived];

                    Assert(ClusterLogs.Logs[leaderOfTerm].Contains(entry));
                }
            }
        }

        private void RecordLogEntryApplication(Event e)
        {
            var logEntryApplication = e as LogEntryAppliedEvent;
            ClusterLogs.AppendToLog(logEntryApplication.ServerId, logEntryApplication.Entry);
        }
    }
}
