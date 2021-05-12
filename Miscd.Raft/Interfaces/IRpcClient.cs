using Microsoft.Coyote.Tasks;
using Miscd.Raft.RPCTypes;

namespace Miscd.Raft.Interfaces
{
    public interface IRpcClient
    {
        Task<AppendEntriesResponse> AppendEntriesAsync(AppendEntriesRequest request);
        Task<VoteResponse> RequestVoteAsync(VoteRequest request);
    }
}
