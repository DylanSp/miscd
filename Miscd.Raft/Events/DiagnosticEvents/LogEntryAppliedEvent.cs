using Microsoft.Coyote;
using System;
using System.Collections.Generic;
using System.Text;

namespace Miscd.Raft.Events.DiagnosticEvents
{
    public class LogEntryAppliedEvent : Event
    {
        public LogEntry Entry { get; }

        public Term Term { get; }

        public LogEntryAppliedEvent(LogEntry entry, Term term)
        {
            Entry = entry;
            Term = term;
        }
    }
}
