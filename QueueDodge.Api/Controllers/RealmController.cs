using Microsoft.AspNet.Mvc;
using QueueDodge.Models;
using QueueDodge.Services;
using System.Collections.Generic;
using System.Linq;

namespace QueueDodge.Api.Controllers
{
    //[RoutePrefix("api/region/{regionID}")]
    [Route("api/realm")]
    public class RealmController : Controller
    {
        private RealmService realms;

        public RealmController()
        {
            realms = new RealmService();
        }

        [HttpGet]
        [Route("realm")]
        public IEnumerable<Realm> Get(int regionID)
        {
            var selectedRegion = (BattleDotSwag.Region)regionID;
            return realms.GetRealms(selectedRegion).OrderBy(p => p.Name);
        }
    }
}
