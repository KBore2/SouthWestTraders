using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace SouthWestTradersAPI.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : Controller
    {
        
        [HttpGet]
        [Route("/errors")]
        public IActionResult HandleErrors()
        {
            var contextException = HttpContext.Features.Get<IExceptionHandlerFeature>();

            var responseStatusCode = contextException?.Error.GetType().Name switch
            {
                "Exception" => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.ServiceUnavailable
            };

            return Problem(detail: contextException?.Error.Message, statusCode: (int)responseStatusCode);
        }
    }
}
