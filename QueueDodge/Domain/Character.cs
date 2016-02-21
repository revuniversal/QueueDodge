namespace QueueDodge
{
    public class Character
    {
        public int ID { get; set; }
        public string Name { get; }
        public int Gender { get; }
        public Realm Realm { get; }
        public Race Race { get; }
        public Class Class { get; }
        public Specialization Specialization { get; }

        public Character(string name,
            int gender,
            Realm realm,
            Race race,
            Class characterClass,
            Specialization specialization)
        {
            Name = name;
            Gender = gender;
            Realm = realm;
            Race = race;
            Class = characterClass;
            Specialization = specialization;
        }
    }
}
