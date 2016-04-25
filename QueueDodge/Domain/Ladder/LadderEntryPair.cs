using System.ComponentModel.DataAnnotations.Schema;

namespace QueueDodge
{
    [NotMapped]
    public class LadderEntryPair
    {
        public LadderEntry Current { get; }
        public LadderEntry Cached { get; }
        
        public LadderEntryPair(LadderEntry current, LadderEntry cached)
        {
            Current = current;
            Cached = cached;
        }
    }
}