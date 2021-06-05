using AutoMapper;

namespace Crowdly_BE.Models.Authentication
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<LoginModel, Services.Authentication.Models.LoginModel>();
            CreateMap<RegisterModel, Services.Authentication.Models.RegisterModel>();
            CreateMap<UpdateUserModel, Services.Authentication.Models.UpdateUserModel>();
            CreateMap<ChangePasswordModel, Services.Authentication.Models.ChangePasswordModel>();

            CreateMap< Services.Authentication.Models.LoginResponse, LoginResponse>();
        }
    }
}
