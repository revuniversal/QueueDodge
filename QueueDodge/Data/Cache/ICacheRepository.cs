using QueueDodge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueDodge.Data.Cache
{
    interface ICacheRepository
    {
        void InsertLadder(LadderKey key, IEnumerable<LadderEntry> Ladder);
        void InsertLadderEntry(LadderEntryKey key, LadderEntry entry);
    }
}
