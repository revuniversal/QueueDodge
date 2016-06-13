using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace QueueDodge.Api.Controllers
{
    [Route(@"api/region/{region}/realm/{realm}/character/{character}")]
    public class CharacterController : Controller
    {
        private QueueDodgeDB queueDodge;

        public CharacterController(QueueDodgeDB queueDodge)
        {
            this.queueDodge = queueDodge;
        }

        [HttpGet]
        public async Task<IEnumerable<Character>> Get(string region, string realm, string character)
        {
            if (string.IsNullOrEmpty(character))
            {
                return new List<Character>();
            }

            var foundPlayers = await queueDodge
                 .Characters
                 .Include(p => p.Race)
                 .Include(p => p.Class)
                 .Include(p => p.Specialization)
                 .Include(p => p.Realm)
                 .Include(p => p.Realm.Region)
                 .Where(p =>
                 ((realm.ToLower() == "all") || (p.Realm.Name.ToLower() == realm.ToLower() || p.Realm.Slug.ToLower() == realm.ToLower()))
                 && p.Name.ToLower().Contains(character.ToLower()))
                 .OrderBy(p => p.Name)
                 .Take(10)
                 .ToListAsync();

            return foundPlayers;
        }
    }
}
