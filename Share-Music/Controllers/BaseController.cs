using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Share_Music.Controllers
{
    public class BaseController : ControllerBase
    {
        protected string? userId
        {
            get
            {
                return HttpContext.User.Identity.IsAuthenticated ?
                    HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier).ToString() : string.Empty;
            }
        }
        protected string? userName
        {
            get
            {
                return HttpContext.User.Identity.IsAuthenticated ?
                    HttpContext.User.FindFirstValue(ClaimTypes.Name).ToString() : string.Empty;
            }
        }
        protected string userRole
        {
            get
            {
                return HttpContext.User.Identity.IsAuthenticated ?
                    HttpContext.User.FindFirstValue(ClaimTypes.Role).ToString():string.Empty;
            }
        }
    }
}
