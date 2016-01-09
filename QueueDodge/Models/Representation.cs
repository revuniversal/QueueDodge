namespace QueueDodge.Models
{
    public class Representation <T>
    {
        public T Data { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }
    }
}
