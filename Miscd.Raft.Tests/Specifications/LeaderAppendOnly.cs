using Microsoft.Coyote;
using Microsoft.Coyote.Specifications;
using Miscd.Raft.Events;
using Miscd.Raft.Events.DiagnosticEvents;
using System.Collections.Generic;

namespace Miscd.Raft.Tests.Specifications
{
    // A leader never overwrites or deletes entries in its log; it only appends new entries
    public class LeaderAppendOnly : Monitor
    {
        private Dictionary<Term, RaftServerId> LeadersByTerm { get; } = new Dictionary<Term, RaftServerId>();

        [Start]
        [OnEventDoAction(typeof(LeaderElectedEvent), nameof(RecordLeaderElection))]
        [OnEventDoAction(typeof(LogOverwrittenEvent), nameof(CheckProperty))]
        [IgnoreEvents(
            typeof(AppendEntriesRequestEvent),
            typeof(AppendEntriesResponseEvent),
            typeof(RequestFromClientEvent),
            typeof(RespondToClientEvent),
            typeof(VoteRequestEvent),
            typeof(VoteResponseEvent),
            typeof(LogEntryAppliedEvent)
        )]
        private class Monitoring : State
        {
        }

        // TODO - same as in LeaderCompleteness - provide some abstract class for monitors that track elections, which they inherit from?
        private void RecordLeaderElection(Event e)
        {
            var election = e as LeaderElectedEvent;
            LeadersByTerm[election.Term] = election.LeaderId;
        }

        private void CheckProperty(Event e)
        {
            var logOverwrite = e as LogOverwrittenEvent;

            Assert(LeadersByTerm[logOverwrite.Term] != logOverwrite.OverwritingServerId);
        }
    }
}
