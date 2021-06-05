using Services.User.Models;
using System;
using System.Threading.Tasks;

namespace Services.User
{
    public interface IUserService
    {
        Task<LoginResponse> LoginAsync(LoginModel loginModel);
        Task<LoginResponse> RegisterAsync(RegisterModel registerModel);
        Task<LoginResponse> UpdateUserAsync(Guid userId, UpdateUserModel updateUser);
        Task<LoginResponse> ChangePasswordAsync(Guid userId, ChangePasswordModel changePassword);
        Task<LoginResponse> UpdateAvatarAsync(Guid userId, string imageName);
    }
}
