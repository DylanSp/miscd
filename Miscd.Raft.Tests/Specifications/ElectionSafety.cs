using Microsoft.Coyote;
using Microsoft.Coyote.Specifications;
using Miscd.Raft.Events;
using Miscd.Raft.Events.DiagnosticEvents;
using System;
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
            typeof(ReceiveAppendEntriesRequestEvent),
            typeof(AppendEntriesResponseEvent),
            typeof(RequestFromClientEvent),
            typeof(RespondToClientEvent),
            typeof(ReceiveVoteRequestEvent),
            typeof(VoteResponseEvent),
            typeof(LogEntryAppliedEvent),
            typeof(LogOverwrittenEvent)
        )]
        private class Monitoring : State
        {
        }

        private void RecordLeaderElection(Event e)
        {
            if (e is not LeaderElectedEvent election)
            {
                throw new Exception($"Incorrect event type passed to {nameof(RecordLeaderElection)} event handler in {nameof(ElectionSafety)} monitor state");
            }

            if (Elections.ContainsKey(election.Term) && Elections[election.Term] != election.LeaderId)
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
