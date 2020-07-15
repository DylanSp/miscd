using Microsoft.Coyote.Specifications;

namespace Miscd.Raft.Tests.Specifications
{
    // If a server has applied a log entry at a given index to its state machine, no other server will ever apply a different log entry for the same index
    public class StateMachineSafety : Monitor
    {
    }
}
