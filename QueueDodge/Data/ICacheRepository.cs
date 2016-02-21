using System.Collections.Generic;
using QueueDodge;
namespace QueueDodge.Data
{
    interface ICacheRepository
    {
        void InsertLadder(LadderKey key, IEnumerable<LadderEntry> Ladder);
        void InsertLadderEntry(LadderEntryKey key, LadderEntry entry);
    }
}
