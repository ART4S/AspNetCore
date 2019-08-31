using AutoMapper;
using Entities.Model;

namespace Web.Features.Registration
{
    public class RegistrationProfile : Profile
    {
        public RegistrationProfile()
        {
            CreateMap<RegisterUser, User>(MemberList.Source)
                .ForSourceMember(x => x.Password, y => y.DoNotValidate());
        }
    }
}
