using Microsoft.AspNet.Mvc;
using QueueDodge.Models;
using QueueDodge.Services;
using System.Collections.Generic;

namespace QueueDodge.Api.Controllers
{
    [Route("api/[controller]")]
    public class ClassController : Controller
    {
        private ClassService classes;

        public ClassController()
        {
            classes = new ClassService();
        }

        public IEnumerable<Class> GetClasses()
        {
            return classes.GetClasses();
        }
    }
}
