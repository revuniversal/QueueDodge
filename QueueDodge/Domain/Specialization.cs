using System.ComponentModel.DataAnnotations.Schema;

namespace QueueDodge
{
    public class Specialization
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string Name { get; set; }

        public Specialization() {}
        public Specialization(int id)
        {
            ID = id;
        }
        public Specialization(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}
