using Microsoft.AspNet.Mvc;
using QueueDodge.Models;
using QueueDodge.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QueueDodge.Api.Controllers
{
    [Route("api/region/{region}/[controller]")]
    public class RealmController : Controller
    {
        private RealmService realms;

        public RealmController()
        {
            realms = new RealmService();
        }

        [HttpGet]
        public IEnumerable<Realm> Get(string region)
        {
            var selectedRegion = (BattleDotSwag.Region)Enum.Parse(typeof(BattleDotSwag.Region),region);
            var data = realms.GetRealms(selectedRegion).OrderBy(p => p.Name).AsEnumerable();
            return data;
        }
    }
}
