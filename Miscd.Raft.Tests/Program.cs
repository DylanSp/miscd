using Microsoft.Coyote.Actors;
using Microsoft.Coyote.SystematicTesting;
using Miscd.Raft.Tests.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miscd.Raft.Tests
{

    public static class Program
    {
        [Test]
        public static void Execute(IActorRuntime runtime)
        {
            // properties from Raft paper figure 3
            runtime.RegisterMonitor<ElectionSafety>();
            runtime.RegisterMonitor<LeaderAppendOnly>();
            runtime.RegisterMonitor<LogMatching>();
            runtime.RegisterMonitor<LeaderCompleteness>();
            runtime.RegisterMonitor<StateMachineSafety>();
        }
    }
}
