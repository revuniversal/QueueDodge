using Microsoft.AspNet.Mvc;
using QueueDodge.Models;
using QueueDodge.Services;

namespace QueueDodge.Api.Controllers
{
    // [RoutePrefix("api/region/{regionID}/realm/{realmID}/character")]
    [Route("api/character")]
    public class CharacterController : Controller
    {
        private CharacterService characters;

        public CharacterController()
        {
            characters = new CharacterService();
        }

        [HttpGet]
        [Route("{name}")]
        public Character GetCharacters(int regionID, int realmID, string name)
        {
            return characters.GetCharacter(regionID, realmID, name);
        }
    }
}
