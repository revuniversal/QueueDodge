using Microsoft.AspNet.Mvc;
using QueueDodge.Models;
using QueueDodge.Services;
using System.Collections.Generic;

namespace QueueDodge.Api.Controllers
{
    [Route("api/[controller]")]
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
