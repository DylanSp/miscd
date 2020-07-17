using Microsoft.Coyote;
using Microsoft.Coyote.Actors;
using Miscd.Raft.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

        #region Public properties (for specification monitors)

        public ReadOnlyCollection<LogEntry> ReadOnlyLog
        {
            get
            {
                return Log.AsReadOnly();
            }
        }

        #endregion

        // TODO - may not need these. create TaskCompletionSource<ClientResponseEvent>(), awaited .Task of that will yield event,
        // ServerOrchestrator uses that to extract response from Raft cluster
        // 
        // if I do need these to send events across network, initialize them by passing them as part of event in ServerOrchestrator's runtime.CreateActor call,
        // don't have a reference to the RaftServer object to assign them 
        #region Callbacks (assigned by ServerOrchestrator)

        public Action SendAppendEntriesAction { get; set; } = () => { };

        public Action SendVoteRequestAction { get; set; } = () => { };

        #endregion

        #region State machine states

        // TODO - add timer, TimerElapsedEvent, handler(s) for that event

        [Start]
        [OnEntry(nameof(BecomeFollower))]
        [OnEventDoAction(typeof(VoteRequestEvent), nameof(RespondToVoteRequest))]
        [OnEventDoAction(typeof(VoteResponseEvent), nameof(UpdateLocalState))] // not candidate, so don't transition states, just update local state
        [OnEventDoAction(typeof(AppendEntriesRequestEvent), nameof(AcceptEntriesAsFollower))]
        [OnEventDoAction(typeof(AppendEntriesResponseEvent), nameof(UpdateLocalState))] // not leader, so don't update log, just update local state
        [OnEventDoAction(typeof(RequestFromClientEvent), nameof(RedirectClientToLeader))]
        private class Follower : State { }
        
        [OnEntry(nameof(BecomeCandidate))]
        [OnEventDoAction(typeof(VoteRequestEvent), nameof(RespondToVoteRequest))]
        [OnEventDoAction(typeof(VoteResponseEvent), nameof(AcceptVoteResponse))]
        [OnEventDoAction(typeof(AppendEntriesRequestEvent), nameof(AcceptEntriesAsCandidate))]
        [OnEventDoAction(typeof(AppendEntriesResponseEvent), nameof(UpdateLocalState))] // not leader, so don't update log, just update local state
        [IgnoreEvents(typeof(RequestFromClientEvent))]  // don't have a leader to redirect client to; ignore and have client retry
        private class Candidate : State { }
        
        [OnEntry(nameof(BecomeLeader))]
        [OnEventDoAction(typeof(VoteRequestEvent), nameof(RespondToVoteRequest))]
        [OnEventDoAction(typeof(VoteResponseEvent), nameof(UpdateLocalState))] // not candidate, so don't transition states, just update local state
        [OnEventDoAction(typeof(AppendEntriesRequestEvent), nameof(UpdateLocalState))] // TODO - is this correct? do I need to resend event and accept entries as follower?
        [OnEventDoAction(typeof(AppendEntriesResponseEvent), nameof(AcceptAppendEntriesResponse))]
        [OnEventDoAction(typeof(RequestFromClientEvent), nameof(RespondToClientRequest))]
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

        // follow rules for RequestVote RPC -> Receiver implementation
        private void RespondToVoteRequest(Event e)
        {
            throw new NotImplementedException();
        }

        // follow rules for Rules for Servers -> Candidates
        private void AcceptVoteResponse(Event e)
        {
            throw new NotImplementedException();
        }

        // follow Rules for Servers -> All Servers
        // * If commitIndex > lastApplied: increment lastApplied, apply log[lastApplied] to state machine
        // * If RPC request or response contains term T > currentTerm: set currentTerm = T, convert to follower(§5.1)
        private void UpdateLocalState(Event e)
        {
            throw new NotImplementedException();
        }

        // follow Rules for Servers -> All Servers/Followers, rules for AppendEntries RPC -> Receiver implementation
        private void AcceptEntriesAsFollower(Event e)
        {
            throw new NotImplementedException();
        }

        // follow Rules for Servers -> All Servers/Candidates, rules for AppendEntries RPC -> Receiver implementation
        private void AcceptEntriesAsCandidate(Event e)
        {
            throw new NotImplementedException();
        }

        // follow Rules for Servers -> All Servers/Leaders
        private void AcceptAppendEntriesResponse(Event e)
        {
            throw new NotImplementedException();
        }

        private void RedirectClientToLeader(Event e)
        {
            throw new NotImplementedException();
        }

        // follow Rules for Servers -> Leaders (bullet 2)
        private void RespondToClientRequest(Event e)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
