using System.Collections.Generic;

namespace Miscd.Raft.Tests
{
    public class RaftClusterLogs
    {
        public Dictionary<RaftServerId, List<LogEntry>> Logs { get; } = new Dictionary<RaftServerId, List<LogEntry>>();

        public void AppendToLog(RaftServerId serverId, LogEntry logEntry)
        {
            if (!Logs.ContainsKey(serverId))
            {
                Logs[serverId] = new List<LogEntry>();
            }

            Logs[serverId].Add(logEntry);
        }
    }
}
