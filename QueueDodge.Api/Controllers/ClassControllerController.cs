using Microsoft.AspNet.Mvc;
using QueueDodge.Models;
using QueueDodge.Services;
using System.Collections.Generic;

namespace QueueDodge.Api.Controllers
{
    [Route("api/classes")]
    public class ClassControllerController : Controller
    {
        private ClassService classes;

        public ClassControllerController()
        {
            classes = new ClassService();
        }

        public IEnumerable<Class> GetClasses()
        {
            return classes.GetClasses();
        }
    }
}
