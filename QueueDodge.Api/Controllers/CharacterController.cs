using Microsoft.AspNet.Mvc;
using QueueDodge.Models;
using QueueDodge.Services;
using System;

namespace QueueDodge.Api.Controllers
{
    [Route("api/region/{region}/realm/{realm}/[controller]")]
    public class CharacterController : Controller
    {
        private CharacterService characters;

        public CharacterController()
        {
            characters = new CharacterService();
        }

        [HttpGet]
        [Route("{name}")]
        public Character GetCharacters(string region, string realm, string name)
        {
            var selectedRegion = (BattleDotSwag.Region)Enum.Parse(typeof(BattleDotSwag.Region), region);
            return characters.GetCharacter(selectedRegion, realm, name);
        }
    }
}
