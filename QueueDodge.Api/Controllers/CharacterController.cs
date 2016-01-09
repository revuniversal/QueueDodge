using Microsoft.AspNet.Mvc;
using QueueDodge.Models;
using QueueDodge.Services;

namespace QueueDodge.Api.Controllers
{
    // [RoutePrefix("api/region/{regionID}/realm/{realmID}/character")]
    [Route("{realm}/[controller]")]
    public class CharacterController : Controller
    {
        private CharacterService characters;

        public CharacterController()
        {
            characters = new CharacterService();
        }

        [HttpGet]
        [Route("{name}")]
        public Character GetCharacters(int region, int realm, string name)
        {
            return characters.GetCharacter(region, realm, name);
        }
    }
}
