using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace WannaBePrincipal.Controllers
{
    public class ErrorController : Controller
    {

        /// <summary>
        /// Error page to handle middleware exceptions
        /// </summary>
        [Route("error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        [ExcludeFromCodeCoverage]
        public IActionResult HandleError()
        {
            return Problem("An internal error occurred. Try again later!");
        }
    }
}
