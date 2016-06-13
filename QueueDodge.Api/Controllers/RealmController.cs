using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace QueueDodge.Api.Controllers
{
    [Route("api/region/{region}/realm")]
    public class RealmController : Controller
    {
        private QueueDodgeDB queueDodge;

        public RealmController(QueueDodgeDB queueDodge)
        {
            this.queueDodge = queueDodge;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Get(string region)
        {
           var realms = await queueDodge
                .Realms
                .Include(p => p.Region)
                .Where(p => p.Region.Name == region)
                .Select(p => p.Name)
                .OrderBy(p => p)
                .ToListAsync();

                realms.Insert(0,"All");
            return realms;
        }
    }
}
