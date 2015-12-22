namespace QueueDodge.Models
{
    public class Character
    {
        public int ID { get; set; }
        public string CharacterName { get; set; }
        public int RegionID { get; set; }
        public int RealmID { get; set; }
        public int Gender { get; set; }
        public int RaceID { get; set; }
        public int ClassID { get; set; }
        public int FactionID { get; set; }
        public int SpecID { get; set; }

        public virtual Region Region { get; set; }
        public virtual Realm Realm { get; set; }
        public virtual Race Race { get; set; }
        public virtual Class Class { get; set; }
        public virtual Faction Faction { get; set; }
        public virtual Specialization Spec { get; set; }

    }
}
