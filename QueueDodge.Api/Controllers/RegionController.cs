using Microsoft.AspNet.Mvc;
using QueueDodge.Models;
using QueueDodge.Services;
using System.Collections.Generic;

namespace QueueDodge.Api.Controllers
{
    // [RoutePrefix("api/region")]
    [Route("api/region")]
    public class RegionController : Controller
    {
        private RegionService regions;

        public RegionController()
        {
            regions = new RegionService();
        }

        [HttpGet]
        public IEnumerable<Region> GetRegions()
        {
            return regions.GetRegions();
        }
    }
}
