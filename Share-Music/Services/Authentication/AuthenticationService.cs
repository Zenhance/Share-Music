using EmailService.Messages;
using EmailService.Services;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Share_Music.DTOs;
using Share_Music.DTOs.Login;
using Share_Music.DTOs.Register;
using Share_Music.Models;
using Share_Music.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Share_Music.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository<User> userRepository;
        private readonly IEmailSender emailSender;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public AuthenticationService(IRepository<User> userRepository,IEmailSender emailSender, UserManager<User>userManager, IMapper mapper, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.emailSender = emailSender;
            this.userManager = userManager;
            this.mapper = mapper;
            this.configuration = configuration;
        }
        public async Task<Response<UserLoginResponseDto>> Login(UserLoginRequestDto userLoginRequest)
        {
            if (userRepository.HasAny(u => u.UserName == userLoginRequest.UserName))
            {
                var user = userRepository.GetByFilter(u => u.UserName == userLoginRequest.UserName).FirstOrDefault();
                
                if(VerifyPasswordHash(userLoginRequest.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return Response.Success((user, CreateToken(user)).Adapt<UserLoginResponseDto>(), "Login successful");
                }
                else
                {
                    return Response.Error<UserLoginResponseDto>("Invalid Credentials");
                }
            }
            else
            {
                return Response.Error<UserLoginResponseDto>("Invalid Credentials");
            }
        }

        public async Task<Response<UserSignUpResponseDto>> Signup(UserSignUpRequestDto userSignUpRequest)
        {
            if (userRepository.HasAny(u => u.UserName == userSignUpRequest.UserName
                || u.Email == userSignUpRequest.Email))
            {
                return (Response<UserSignUpResponseDto>)Response.Error("UserName/Email already Exists");
            }
            else
            {
                CreatePasswordHash(userSignUpRequest.Password, out byte[] passwordHash, out byte[] passwordSalt);

                User newUser = mapper.Map<User>(userSignUpRequest);
                newUser.PasswordSalt = passwordSalt;
                newUser.PasswordHash = passwordHash;

                var emailConfirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var emailConfirmationLink = "https://localhost:7184/api/Authentication/ConfirmEmailLink?token=" + emailConfirmationToken + "&email=" + newUser.Email;
                var emailConfirmationMessage = new MailKitMailMessage(new string[] { newUser.Email }, "Verification Link", emailConfirmationLink , null);

                await Task.WhenAll(
                    userRepository.CreateAsync(newUser),
                    emailSender.SendEmailAsync(emailConfirmationMessage)
                    );

                return Response.Success(mapper.Map<UserSignUpResponseDto>(newUser), "User created successfully");
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

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("TokenSettings:Secret").Value));
            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = signingCredentials
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}


