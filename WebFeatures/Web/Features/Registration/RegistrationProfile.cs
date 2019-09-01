using AutoMapper;
using Entities.Model;

namespace Web.Features.Registration
{
    /// <summary>
    /// Настройка маппинга команд/запросов регистрации
    /// </summary>
    public class RegistrationProfile : Profile
    {
        /// <inheritdoc />
        public RegistrationProfile()
        {
            CreateMap<RegisterUserCommand, User>(MemberList.Source)
                .ForSourceMember(x => x.Password, y => y.DoNotValidate())
                .ForSourceMember(x => x.ConfirmPassword, y => y.DoNotValidate());
        }
    }
}
