using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("/error")]
        [HttpGet] //Will be removed later. Placed for swaggerUI.
        public IActionResult Error() => Problem();
    }
}