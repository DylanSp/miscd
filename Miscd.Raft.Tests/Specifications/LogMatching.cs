using Microsoft.Coyote.Specifications;

namespace Miscd.Raft.Tests.Specifications
{
    // If two logs contain an entry with the same index and term, then the logs are identical in all entries up through the given index
    public class LogMatching : Monitor
    {
    }
}
