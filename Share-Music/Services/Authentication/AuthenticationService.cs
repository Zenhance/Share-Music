using MapsterMapper;
using Share_Music.Data;
using Share_Music.DTOs;
using Share_Music.DTOs.Login;
using Share_Music.DTOs.Register;
using Share_Music.Models;
using Share_Music.Repositories;

namespace Share_Music.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository<User> userRepository;
        private readonly IMapper mapper;

        public AuthenticationService(IRepository<User> userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }
        public Task<Response<UserLoginResponseDto>> Login(UserLoginRequestDto userLoginRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<UserSignUpResponseDto>> Signup(UserSignUpRequestDto userSignUpRequest)
        {
            if(userRepository.HasAny(u=> u.UserName==userSignUpRequest.UserName 
                || u.Email==userSignUpRequest.Email))
            {
                return (Response<UserSignUpResponseDto>)Response.Error("User already Exists");
            }
            else
            {
                CreatePasswordHash(userSignUpRequest.Password, out byte[] passwordHash, out byte[] passwordSalt);
                
                User newUser = mapper.Map<User>(userSignUpRequest);
                newUser.PasswordSalt = passwordSalt;
                newUser.PasswordHash = passwordHash;

                await userRepository.CreateAsync(newUser);

                return Response.Success(mapper.Map<UserSignUpResponseDto>(newUser),"User created successfully");
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSlat)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSlat = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
