using Share_Music.DTOs;
using Share_Music.DTOs.Login;
using Share_Music.DTOs.Register;

namespace Share_Music.Services.Authentication
{
    public interface IAuthenticationService
    {
        public Task<Response<UserSignUpResponseDto>> Signup(UserSignUpRequestDto userSignUpResponse);
        public Task<Response<UserLoginResponseDto>> Login(UserLoginRequestDto userLoginResponse);
        public Task<bool> IsEmailVerified(string token, string email);
    }
}
