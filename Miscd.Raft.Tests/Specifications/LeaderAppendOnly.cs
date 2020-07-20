using Microsoft.Coyote.Specifications;

namespace Miscd.Raft.Tests.Specifications
{
    // A leader never overwrites or deletes entries in its log; it only appends new entries
    // TODO - does this mean I have to make Log publicly gettable?
    public class LeaderAppendOnly : Monitor
    {
    }
}
