using AutoMapper;
using Data.Model;
using Security.Dto;
using Security.Models;

namespace Security.Profiles
{
    /// <summary>
    /// Профиль маппера для аккаунта
    /// </summary>
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountDto>(MemberList.Destination);
            CreateMap<RegisterModel, Account>(MemberList.None)
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src.Login));
        }
    }
}
