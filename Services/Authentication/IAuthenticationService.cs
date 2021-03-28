using Services.Authentication.Models;
using System.Threading.Tasks;

namespace Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> LoginAsync(LoginModel loginModel);
        Task<string[]> RegisterAsync(RegisterModel registerModel);
    }
}
