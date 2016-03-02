namespace QueueDodge
{
    public class Character
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public int RealmID { get; set; }
        public int RaceID { get; set; }
        public int ClassID { get; set; }
        public int SpecializationID { get; set; }

        public virtual Realm Realm { get; set; }
        public virtual Race Race { get; set; }
        public virtual Class Class { get; set; }
        public virtual Specialization Specialization { get; set; }
        public Character() { }
        public Character(string name,
            int gender,
            int realmId,
            int raceId,
            int characterClassId,
            int specializationId)
        {
            Name = name;
            Gender = gender;
            RealmID = realmId;
            RaceID = raceId;
            ClassID = characterClassId;
            SpecializationID = specializationId;
            
        }
    }
}
