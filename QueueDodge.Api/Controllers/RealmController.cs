using Microsoft.AspNet.Mvc;
using QueueDodge.Models;
using QueueDodge.Services;
using System.Collections.Generic;
using System.Linq;

namespace QueueDodge.Api.Controllers
{
    //[RoutePrefix("api/region/{regionID}")]
    [Route("api/region")]
    public class RealmController : Controller
    {
        private RealmService realms;

        public RealmController()
        {
            realms = new RealmService();
        }

        [HttpGet("{region}/[controller]")]
        public IEnumerable<Realm> Get(int region)
        {
            var selectedRegion = (BattleDotSwag.Region)region;
            var data = realms.GetRealms(selectedRegion).OrderBy(p => p.Name).AsEnumerable();
            return data;
        }
    }
}
