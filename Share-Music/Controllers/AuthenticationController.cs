using EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Share_Music.DTOs.Login;
using Share_Music.DTOs.Register;
using Share_Music.Models;
using Share_Music.Services.Authentication;

namespace Share_Music.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService authenticationService;
        private readonly UserManager<User> userManager;

        public AuthenticationController(IAuthenticationService authenticationService, UserManager<User>userManager)
        {
            this.authenticationService = authenticationService;
            this.userManager = userManager;
        }

        [HttpPost("Signup")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserSignUpResponseDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Signup([FromBody]UserSignUpRequestDto userInfo)
        {
            if (ModelState.IsValid)
            {
                var response = await authenticationService.Signup(userInfo);

                if(response.IsSuccess==true)
                {
                    return Ok(response.Data);
                }
                else
                {
                    return BadRequest(response.Message);
                }
            }
            return BadRequest();
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserLoginResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto userInfo)
        {
            if(ModelState.IsValid)
            {
                var response = await authenticationService.Login(userInfo);
                if (response.IsSuccess == true)
                {
                    return Ok(response.Data);
                }
                else
                {
                    return BadRequest(response.Message);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("ValidateToken")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        public ActionResult ValidateToken()
        {
            return Ok(new { isValid = HttpContext.User.Identity.IsAuthenticated });
        }

        [HttpGet("VerifyEmail")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> VerifyEmail(string token, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest();
            var result = await userManager.ConfirmEmailAsync(user, token);
            
            return result.Succeeded ? Ok("Email Confirmed") : BadRequest();
        }
    }
}
