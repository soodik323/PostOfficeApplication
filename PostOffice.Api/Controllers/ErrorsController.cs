using System.Net;
using Microsoft.AspNetCore.Mvc;
using PostOffice.Application.Responses;

namespace PostOffice.Api.Controllers
{
    [Route("/errors")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        [Route("{code}")]
        public IActionResult Error(int code)
        {
            HttpStatusCode parsedCode = (HttpStatusCode)code;

            var returnValue = new ErrorResponse((int) parsedCode, parsedCode.ToString());

            return new ObjectResult(returnValue);
        }
    }
}

