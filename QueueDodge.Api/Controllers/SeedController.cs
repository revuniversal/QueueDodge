using Microsoft.AspNet.Mvc;
using QueueDodge.Data;

namespace QueueDodge.Api.Controllers
{
    // HACK: TODO:  DO NOT LET THIS GO INTO PRODUCTION OR THE CONSEQUENCES WILL BE DIRE. MAKE A CONSOLE APP OR SOMETHING. - Jesus
    [Route("api/seed")]
    public class SeedController : Controller
    {
        private QueueDodgeDB queueDodge;
        private QueueDodgeSeed seed;

        public SeedController(QueueDodgeDB queueDodge, QueueDodgeSeed seed)
        {
            this.queueDodge = queueDodge;
            this.seed = seed;
        }

        [HttpGet]
        [Route("")]
        public HttpOkResult Seed()
        {
            seed.Seed();
            return Ok();
        }
    }
}
