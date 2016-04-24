using QueueDodge;

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