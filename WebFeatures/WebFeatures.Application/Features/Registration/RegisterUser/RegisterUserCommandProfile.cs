using AutoMapper;
using WebFeatures.Domian.Entities.Model;

namespace WebFeatures.Application.Features.Registration.RegisterUser
{
    public class RegisterUserCommandProfile : Profile
    {
        public RegisterUserCommandProfile()
        {
            CreateMap<RegisterUserCommand, User>(MemberList.Source)
                .ForSourceMember(x => x.Password, y => y.DoNotValidate())
                .ForSourceMember(x => x.ConfirmPassword, y => y.DoNotValidate());
        }
    }
}
