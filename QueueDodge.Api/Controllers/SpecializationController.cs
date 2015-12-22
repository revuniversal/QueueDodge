using Microsoft.AspNet.Mvc;
using QueueDodge.Models;
using QueueDodge.Services;
using System.Collections.Generic;

namespace QueueDodge.Api.Controllers
{
    [Route("api/specialization")]
    public class SpecializationController : Controller
    {
        private SpecializationService specializations;

        public SpecializationController()
        {
            specializations = new SpecializationService();
        }
        public IEnumerable<Specialization> GetSpecializations()
        {
            return specializations.GetSpecializations();
        }
    }
}
