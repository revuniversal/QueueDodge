﻿using System.ComponentModel.DataAnnotations.Schema;

namespace QueueDodge
{
    public class Region
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }
        public string Name { get; set; }
        public Region(int id, string name)
        {
            ID = id;
            Name = name;
        }
        public Region() { }
    }
}
