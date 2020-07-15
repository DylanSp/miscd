using Microsoft.Coyote.Actors;
using System;
using System.Collections.Generic;
using System.Text;

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
        private class Follower : State { }
        
        private class Candidate : State { }
        
        private class Leader : State { }

        #endregion
    }
}
