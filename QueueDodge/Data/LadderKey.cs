namespace QueueDodge.Data
{
    public class LadderKey
    {
        private string Bracket { get; set; }
        private string Region { get; set; }
        public LadderKey(string bracket, string region)
        {
            Bracket = bracket;
            Region = region;
        }

    }
}
