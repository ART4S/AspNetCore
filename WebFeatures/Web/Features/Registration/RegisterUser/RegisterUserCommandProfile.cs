using AutoMapper;
using WebFeatures.Entities.Model;

namespace Web.Features.Registration.RegisterUser
{
    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public class RegisterUserCommandProfile : Profile
    {
        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public RegisterUserCommandProfile()
        {
            CreateMap<RegisterUserCommand, User>(MemberList.Source)
                .ForSourceMember(x => x.Password, y => y.DoNotValidate())
                .ForSourceMember(x => x.ConfirmPassword, y => y.DoNotValidate());
        }
    }
}
