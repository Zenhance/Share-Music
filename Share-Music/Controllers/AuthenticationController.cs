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

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
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
           
            if(await authenticationService.IsEmailVerified(token,email))
            {
                return Ok("User Verified");
            }
            else
            {
                return BadRequest();
            }
                       
        }


    }
}
