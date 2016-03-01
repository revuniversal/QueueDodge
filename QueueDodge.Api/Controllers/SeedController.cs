using Microsoft.AspNet.Mvc;
using QueueDodge.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueDodge.Api.Controllers
{
    // HACK: TODO:  DO NOT LET THIS GO INTO PRODUCTION OR THE CONSEQUENCES WILL BE DIRE. MAKE A CONSOLE APP OR SOMETHING. - Jesus
    [Route("api/seed")]
    public class SeedController : Controller
    {
        private QueueDodgeDB queueDodge;

        public SeedController(QueueDodgeDB queueDodge)
        {
            this.queueDodge = queueDodge;
        }

        [HttpGet]
        [Route("")]
        public void Seed()
        {
             var seed = new DatabaseSeed(queueDodge);
            seed.Seed();
        }
    }
}
