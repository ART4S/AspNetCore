using AutoMapper;
using WebFeatures.Domian.Entities.Model;
using WebFeatures.Domian.ValueObjects;

namespace WebFeatures.Application.Features.Registration.RegisterUser
{
    public class RegisterUserCommandProfile : Profile
    {
        public RegisterUserCommandProfile()
        {
            CreateMap<RegisterUserCommand, ContactDetails>();
            CreateMap<RegisterUserCommand, User>(MemberList.Source)
                .ForSourceMember(x => x.Password, y => y.DoNotValidate())
                .ForSourceMember(x => x.ConfirmPassword, y => y.DoNotValidate())
                .ForMember(x => x.ContactDetails, y => y.MapFrom(x => x));
        }
    }
}
