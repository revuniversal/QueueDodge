using QueueDodge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueDodge.Services
{
    public class CharacterService
    {
        private QueueDodgeDB data { get; set; }

        public CharacterService()
        {
            data = new QueueDodgeDB();
        }

        public Character GetCharacter(int regionID, int realmID, string name)
        {
            Character character = data.Characters
                .Where(p => p.CharacterName.ToLower() == name.ToLower() &&
                            p.RealmID == realmID &&
                            p.RegionID == regionID)
                .ToList()
                .FirstOrDefault();

            return character;

        }
    }
}
