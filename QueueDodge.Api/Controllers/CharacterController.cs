﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IEnumerable<Character>> Get(string region, string realm, string character)
        {
           var foundPlayers = await queueDodge
                .Characters
                .Where(p =>
                (p.Realm.Name == realm || p.Realm.Slug == realm)
                && p.Realm.Region.Name == region
                && p.Name.Contains(character))
                .ToListAsync();
         
            return foundPlayers;
        }
    }
}
