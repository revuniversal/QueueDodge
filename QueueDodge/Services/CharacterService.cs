using QueueDodge.Models;
using System.Linq;

namespace QueueDodge.Services
{
    public class CharacterService
    {
        private QueueDodgeDB data { get; set; }

        public CharacterService()
        {
            data = new QueueDodgeDB();
        }

        public Character GetCharacter(BattleDotSwag.Region region, string realm, string name)
        {
            Character character = data.Characters
                .Where(p => p.CharacterName.ToLower() == name.ToLower() &&
                            (p.Realm.Name == realm || p.Realm.Slug == realm) &&
                            p.RegionID == (int)region)
                .ToList()
                .FirstOrDefault();

            return character;

        }
    }
}
