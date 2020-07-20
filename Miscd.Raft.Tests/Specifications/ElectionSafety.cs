using Microsoft.Coyote;
using Microsoft.Coyote.Specifications;
using Miscd.Raft.Events;
using Miscd.Raft.Events.DiagnosticEvents;
using System.Collections.Generic;

namespace Miscd.Raft.Tests.Specifications
{
    // At most one leader can be elected in a given term
    public class ElectionSafety : Monitor
    {
        private Dictionary<Term, RaftServerId> Elections { get; } = new Dictionary<Term, RaftServerId>();

        [Start]
        [OnEventDoAction(typeof(LeaderElectedEvent), nameof(RecordLeaderElection))]
        [IgnoreEvents(
            typeof(AppendEntriesRequestEvent),
            typeof(AppendEntriesResponseEvent),
            typeof(RequestFromClientEvent),
            typeof(RespondToClientEvent),
            typeof(VoteRequestEvent),
            typeof(VoteResponseEvent),
            typeof(LogEntryAppliedEvent),
            typeof(LogOverwrittenEvent)
        )]
        private class Monitoring : State
        {
        }

        private void RecordLeaderElection(Event e)
        {
            var election = e as LeaderElectedEvent;

            if (Elections.ContainsKey(election.Term) && !Elections[election.Term].Equals(election.LeaderId))    // TODO convert to !=
            {
                Assert(false);
            }
            else
            {
                Elections[election.Term] = election.LeaderId;
            }
        }
    }
}
