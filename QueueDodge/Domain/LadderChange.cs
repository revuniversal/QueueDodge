namespace QueueDodge
{
    public class LadderChange
    {
        public int ID { get; set; }
        public LadderEntry Previous { get; set; }
        public LadderEntry Current { get; set; }

        public LadderChange(LadderEntry previous, LadderEntry current)
        {
            Previous = previous;
            Current = current;
        }
        public bool Changed() => (Previous.Season.Wins != Current.Season.Wins) || (Previous.Season.Losses != Current.Season.Losses);
    }
}
