using System.Linq;
using Microsoft.AspNet.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace QueueDodge.Api.Controllers
{
    [Route("api/region/{region}/realm/{realm}/character/{character}")]
    public class CharacterController : Controller
    {
        private QueueDodgeDB queueDodge;

        public CharacterController(QueueDodgeDB queueDodge)
        {
            this.queueDodge = queueDodge;
        }

        [HttpGet]
        public Character Get(string region, string realm, string character)
        {
            var player = queueDodge
                .Characters
                .Where(p =>
                (p.Realm.Name == realm || p.Realm.Slug == realm)
                && p.Realm.Region.Name == region
                && p.Name == character)
                .Single();

            return player;
        }
    }
}
