using System.ComponentModel.DataAnnotations.Schema;

namespace QueueDodge
{
    public class Class
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string PowerType { get; set; }
        public Class() { }
        public Class(int id)
        {
            ID = id;
        }
        public Class(int id, string name, string powerType)
        {
            ID = id;
            Name = name;
            PowerType = powerType;
        }
    }
}
