using Share_Music.DTOs.Login;
using Share_Music.DTOs.Register;

namespace Share_Music.Services.Authentication
{
    public interface IAuthenticationService
    {
        public UserSignUpResponseDto Signup(UserSignUpRequestDto userSignUpResponse);
        public UserLoginResponseDto Login(UserLoginRequestDto userLoginResponse);
    }
}
