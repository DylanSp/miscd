using Microsoft.Coyote;
using Microsoft.Coyote.Actors;
using Miscd.Raft.Events;
using System;
using System.Collections.Generic;

namespace Miscd.Raft
{
    public class RaftServer : StateMachine
    {
        #region Persistent state

        // latest term server has seen (initialized to 0 on first boot, increases monotonically)
        private Term CurrentTerm { get; set; } = new Term(0);

        // candidateId that received vote in current term (or null if none) 
        private RaftServerId? CandidateVotedFor { get; set; } = null;

        // log entries
        private List<LogEntry> Log { get; } = new List<LogEntry>();

        #endregion

        #region Volatile state for all states

        // index of highest log entry known to be committed (initialized to 0, increases monotonically)
        private Index CommitIndex { get; set; } = new Index(0);

        // index of highest log entry applied to machine (initialized to 0, increases monotonically)
        private Index LastAppliedLogIndex { get; set; } = new Index(0);

        #endregion

        #region Volatile Leader-only state (re-initialized after election)

        // for each server, index of the next log entry to send to that server (initialized to leader last log index + 1)
        private Dictionary<RaftServerId, Index> NextIndexForServers { get; set; } = new Dictionary<RaftServerId, Index>();

        // for each server, index of highest log entry known to be replicated on server (initialized to 0, increases monotonically)
        private Dictionary<RaftServerId, Index> MatchIndexForServers { get; set; } = new Dictionary<RaftServerId, Index>();

        #endregion


        #region State machine states

        [Start]
        [OnEntry(nameof(BecomeFollower))]
        [OnEventDoAction(typeof(VoteRequestEvent), nameof(RespondToVoteRequest))]
        [OnEventDoAction(typeof(VoteResponseEvent), nameof(UpdateLocalState))]
        [OnEventDoAction(typeof(AppendEntriesRequestEvent), nameof(AcceptEntriesAsFollower))]
        [OnEventDoAction(typeof(AppendEntriesResponseEvent), nameof(UpdateLocalState))]
        private class Follower : State { }
        
        [OnEntry(nameof(BecomeCandidate))]
        [OnEventDoAction(typeof(VoteRequestEvent), nameof(RespondToVoteRequest))]
        [OnEventDoAction(typeof(VoteResponseEvent), nameof(AcceptVoteResponse))]
        [OnEventDoAction(typeof(AppendEntriesRequestEvent), nameof(AcceptEntriesAsCandidate))]
        [OnEventDoAction(typeof(AppendEntriesResponseEvent), nameof(UpdateLocalState))]
        private class Candidate : State { }
        
        [OnEntry(nameof(BecomeLeader))]
        [OnEventDoAction(typeof(VoteRequestEvent), nameof(RespondToVoteRequest))]
        [OnEventDoAction(typeof(VoteResponseEvent), nameof(UpdateLocalState))]
        [OnEventDoAction(typeof(AppendEntriesRequestEvent), nameof(UpdateLocalState))]
        [OnEventDoAction(typeof(AppendEntriesResponseEvent), nameof(AcceptAppendEntriesResponse))]
        private class Leader : State { }

        #endregion

        #region Event handlers

        private void BecomeFollower(Event e)
        {
            throw new NotImplementedException();
        }

        private void BecomeCandidate(Event e)
        {
            throw new NotImplementedException();
        }

        private void BecomeLeader(Event e)
        {
            throw new NotImplementedException();
        }

        private void RespondToVoteRequest(Event e)
        {
            throw new NotImplementedException();
        }

        private void AcceptVoteResponse(Event e)
        {
            throw new NotImplementedException();
        }

        private void UpdateLocalState(Event e)
        {
            throw new NotImplementedException();
        }

        private void AcceptEntriesAsFollower(Event e)
        {
            throw new NotImplementedException();
        }

        private void AcceptEntriesAsCandidate(Event e)
        {
            throw new NotImplementedException();
        }

        private void AcceptAppendEntriesResponse(Event e)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
