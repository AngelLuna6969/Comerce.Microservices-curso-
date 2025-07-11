using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [ApiController]
        [Route("/")]
    public class DefaultController : ControllerBase
    {
        [HttpGet]
        public string Index()
        {
            return "Customers.API it's working ...";
        }
    }
}
