using Microsoft.Coyote.Specifications;

namespace Miscd.Raft.Tests.Specifications
{
    // At most one leader can be elected in a given term
    public class ElectionSafety : Monitor
    {
    }
}
