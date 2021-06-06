using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.User.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<LoginResponse> LoginAsync(LoginModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user is null || !(await _userManager.CheckPasswordAsync(user, loginModel.Password)))
                return new LoginResponse
                {
                    ErrorMessages = new string[] { "Email sau Parola incorecte." }
                };

            JwtSecurityToken token = await CreateAuthTokenAsync(user);

            return new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo,
                ErrorMessages = new string[0]
            };
        }

        public async Task<LoginResponse> RegisterAsync(RegisterModel registerModel)
        {
            var userExists = await _userManager.FindByEmailAsync(registerModel.Email);
            if (userExists != null)
                return new LoginResponse
                {
                    ErrorMessages = new string[] { "Aceasta adresa de email a fost deja inregistrata" }
                };

            ApplicationUser user = new ApplicationUser()
            {
                Email = registerModel.Email,
                UserName = registerModel.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                PhoneNumber = registerModel.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded)
                return new LoginResponse
                {
                    ErrorMessages = result.Errors.Select(err => err.Description).ToArray()
                };

            JwtSecurityToken token = await CreateAuthTokenAsync(user);

            return new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo,
                ErrorMessages = new string[0]
            };
        }

        public async Task<LoginResponse> UpdateUserAsync(Guid userId, UpdateUserModel updateUser)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                return new LoginResponse
                {
                    ErrorMessages = new string[] { "User not existent" }
                };

            user.Email = updateUser.Email;
            user.FirstName = updateUser.FirstName;
            user.LastName = updateUser.LastName;
            user.PhoneNumber = updateUser.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);

            if (result.Errors.Any())
                return new LoginResponse
                {
                    ErrorMessages = result.Errors.Select(err => err.Description).ToArray()
                };


            JwtSecurityToken token = await CreateAuthTokenAsync(user);

            return new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo,
                ErrorMessages = new string[0]
            };
        }

        public async Task<LoginResponse> ChangePasswordAsync(Guid userId, ChangePasswordModel changePassword)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                return new LoginResponse
                {
                    ErrorMessages = new string[] { "User not existent" }
                };

            var result = await _userManager.ChangePasswordAsync(user, changePassword.OldPassword, changePassword.NewPassword);

            if (result.Errors.Any())
                return new LoginResponse
                {
                    ErrorMessages = result.Errors.Select(err => err.Description).ToArray()
                };


            JwtSecurityToken token = await CreateAuthTokenAsync(user);

            return new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo,
                ErrorMessages = new string[0]
            };
        }

        public async Task<LoginResponse> UpdateAvatarAsync(Guid userId, string imageName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                return new LoginResponse
                {
                    ErrorMessages = new string[] { "User not existent" }
                };

            user.Image = imageName;

            var result = await _userManager.UpdateAsync(user);

            if (result.Errors.Any())
                return new LoginResponse
                {
                    ErrorMessages = result.Errors.Select(err => err.Description).ToArray()
                };


            JwtSecurityToken token = await CreateAuthTokenAsync(user);

            return new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo,
                ErrorMessages = new string[0]
            };
        }

        private async Task<JwtSecurityToken> CreateAuthTokenAsync(ApplicationUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            if (user.FirstName is not null)
                authClaims.Add(new Claim("firstName", user.FirstName));
            if (user.LastName is not null)
                authClaims.Add(new Claim("lastName", user.LastName));
            if (user.PhoneNumber is not null)
                authClaims.Add(new Claim(ClaimTypes.OtherPhone, user.PhoneNumber));
            if (user.Image is not null)
                authClaims.Add(new Claim("image", user.Image));

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(100),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
