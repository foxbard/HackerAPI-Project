
using Microsoft.AspNetCore.Mvc;

namespace hackerAPI.Client.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        [HttpGet]
        public ActionResult api()
        {
            return Ok("Api online");
        }
    }
}
