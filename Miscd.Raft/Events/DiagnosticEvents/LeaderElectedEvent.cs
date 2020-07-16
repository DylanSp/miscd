using Microsoft.Coyote;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miscd.Raft.Events.DiagnosticEvents
{
    public class LeaderElectedEvent : Event
    {
        public Term Term { get; }
        public RaftServerId LeaderId { get; }

        public LeaderElectedEvent(Term term, RaftServerId leaderId)
        {
            Term = term;
            LeaderId = leaderId;
        }
    }
}
