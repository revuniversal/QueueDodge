using Microsoft.AspNet.Mvc;

namespace QueueDodge.Api.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        // GET: Home
        public ViewResult Index()
        {
            return View("Index");
        }
    }
}