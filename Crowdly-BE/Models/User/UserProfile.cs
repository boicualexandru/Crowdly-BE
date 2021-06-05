using AutoMapper;

namespace Crowdly_BE.Models.User
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<LoginModel, Services.User.Models.LoginModel>();
            CreateMap<RegisterModel, Services.User.Models.RegisterModel>();
            CreateMap<UpdateUserModel, Services.User.Models.UpdateUserModel>();
            CreateMap<ChangePasswordModel, Services.User.Models.ChangePasswordModel>();

            CreateMap< Services.User.Models.LoginResponse, LoginResponse>();
        }
    }
}
