using Microsoft.Coyote;
using Microsoft.Coyote.Specifications;
using Miscd.Raft.Events;
using Miscd.Raft.Events.DiagnosticEvents;
using System;
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
            if (e is not LeaderElectedEvent election)
            {
                throw new Exception($"Incorrect event type passed to {nameof(RecordLeaderElection)} event handler in {nameof(LeaderAppendOnly)} monitor state");
            }
            LeadersByTerm[election.Term] = election.LeaderId;
        }

        private void CheckProperty(Event e)
        {
            if (e is not LogOverwrittenEvent logOverwrite)
            {
                throw new Exception($"Incorrect event type passed to {nameof(CheckProperty)} event handler in {nameof(LeaderAppendOnly)} monitor state");
            }

            Assert(LeadersByTerm[logOverwrite.Term] != logOverwrite.OverwritingServerId);
        }
    }
}
