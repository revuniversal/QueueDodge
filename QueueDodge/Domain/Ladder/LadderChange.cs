using System.ComponentModel.DataAnnotations.Schema;

namespace QueueDodge
{
    [NotMapped]
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
        public bool Changed() => (Previous.SeasonWins != Current.SeasonWins) || (Previous.SeasonLosses != Current.SeasonLosses);
    }
}
