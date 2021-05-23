using Services.Authentication.Models;
using System.Threading.Tasks;

namespace Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> LoginAsync(LoginModel loginModel);
        Task<LoginResponse> RegisterAsync(RegisterModel registerModel);
    }
}
