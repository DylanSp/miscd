using Microsoft.Coyote.Specifications;

namespace Miscd.Raft.Tests.Specifications
{
    // If a log entry is committed in a given term, then that entry will be present in the logs of the leaders for all higher-numbered terms.
    public class LeaderCompleteness : Monitor
    {
    }
}
