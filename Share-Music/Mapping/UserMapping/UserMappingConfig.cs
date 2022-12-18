using Mapster;
using Share_Music.DTOs.Register;
using Share_Music.Models;

namespace Share_Music.Mapping.UserMapping
{
    public class UserMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UserSignUpRequestDto, User>()
                .Map(dest=> dest.UserName, src => src.UserName)
                .Map(dest => dest.Email, src => src.Email)
                .IgnoreNonMapped(true);

            config.NewConfig<User, UserSignUpResponseDto>()
                .Map(dest => dest.Id, src => src.Id)
                .IgnoreNonMapped(true);
        }
    }
}
