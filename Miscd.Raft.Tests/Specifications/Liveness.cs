using Microsoft.Coyote.Specifications;
using Miscd.Raft.Events;
using Miscd.Raft.Events.DiagnosticEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miscd.Raft.Tests.Specifications
{

#pragma warning disable S125 // Sections of code should not be commented out
    // this property isn't specified explicitly in Raft paper;
    // this is my own interpretation of a liveness property for Raft
#pragma warning restore S125 // Sections of code should not be commented out

    public class Liveness : Monitor
    {
        [Start]
        [Cold]
        [OnEventGotoState(typeof(RequestFromClientEvent), typeof(ReceivedRequest))]
        [IgnoreEvents(
            typeof(ReceiveAppendEntriesRequestEvent),
            typeof(AppendEntriesResponseEvent),
            typeof(ReceiveVoteRequestEvent),
            typeof(VoteResponseEvent),
            typeof(LeaderElectedEvent),
            typeof(LogEntryAppliedEvent),
            typeof(LogOverwrittenEvent)
        )]
        private class WaitingForRequests : State
        {

        }

        [Hot]
        [OnEventGotoState(typeof(RespondToClientEvent), typeof(WaitingForRequests))]
        [IgnoreEvents(
            typeof(ReceiveAppendEntriesRequestEvent),
            typeof(AppendEntriesResponseEvent),
            typeof(ReceiveVoteRequestEvent),
            typeof(VoteResponseEvent),
            typeof(LeaderElectedEvent),
            typeof(LogEntryAppliedEvent),
            typeof(LogOverwrittenEvent)
        )]
        private class ReceivedRequest : State
        {
#pragma warning disable S125 // Sections of code should not be commented out
            // TODO check that sufficient # of replicas are up
            // TODO possibly check that leader is elected?
            /*
             * From Raft paper:
             * "If the leader crashes, client requests will time out;
             * clients then try again with randomly-chosen servers
             */
#pragma warning restore S125 // Sections of code should not be commented out
        }
    }
}
