using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using QueueDodge.Api;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        [HttpGet]
        public async Task<string> Get(string message)
        {
            await WebSocketMiddleware.Broadcast(message);

            return message;
        }
    }
}
