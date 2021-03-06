using AutoMapper;
using Crowdly_BE.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.User;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Crowdly_BE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginModel model)
        {
            var loginResponse = await _userService.LoginAsync(_mapper.Map<Services.User.Models.LoginModel>(model));

            if (loginResponse.ErrorMessages.Any())
                return BadRequest(loginResponse.ErrorMessages);

            return _mapper.Map<LoginResponse>(loginResponse);
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<LoginResponse>> Register([FromBody] RegisterModel model)
        {
            var registerResponse = await _userService.RegisterAsync(_mapper.Map<Services.User.Models.RegisterModel>(model));

            if (registerResponse.ErrorMessages.Any())
                return BadRequest(registerResponse.ErrorMessages);
            
            return _mapper.Map<LoginResponse>(registerResponse);
        }

        [HttpPost]
        [Route("Update")]
        [Authorize]
        public async Task<ActionResult<LoginResponse>> UpdateUser([FromBody] UpdateUserModel updateUser)
        {
            var userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var response = await _userService.UpdateUserAsync(userId, _mapper.Map<Services.User.Models.UpdateUserModel>(updateUser));

            if (response.ErrorMessages.Any())
                return BadRequest(response.ErrorMessages);

            return _mapper.Map<LoginResponse>(response);

        }

        [HttpPost]
        [Route("ChangePassword")]
        [Authorize]
        public async Task<ActionResult<LoginResponse>> ChangePassword([FromBody] ChangePasswordModel changePassword)
        {
            var userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var response = await _userService.ChangePasswordAsync(userId, _mapper.Map<Services.User.Models.ChangePasswordModel>(changePassword));

            if (response.ErrorMessages.Any())
                return BadRequest(response.ErrorMessages);

            return _mapper.Map<LoginResponse>(response);

        }

        [HttpPost]
        [Route("UploadAvatar")]
        [Authorize]
        public async Task<ActionResult<LoginResponse>> UploadAvatar([FromForm] UploadAvatarModel uploadAvatar)
        {
            var userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var oldAvatarImage = User.FindFirstValue("image");

            var imageName = UploadImage(userId, uploadAvatar.FormFile);

            var response = await _userService.UpdateAvatarAsync(userId, imageName);

            if (response.ErrorMessages.Any())
                return BadRequest(response.ErrorMessages);

            if(oldAvatarImage is not null)
                DeleteImages(userId, oldAvatarImage);

            return _mapper.Map<LoginResponse>(response);

        }

        private string UploadImage(Guid userId, IFormFile formFile)
        {
            if (formFile is null) return null;

            var directory = GetOrCreateUserDirectory(userId);

            var fileExtension = Path.GetExtension(formFile.FileName);
            var imageName = Guid.NewGuid().ToString() + fileExtension;

            string path = Path.Combine(directory, imageName);

            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }

            return imageName;
        }

        private void DeleteImages(Guid userId, string imageName)
        {
            var directory = GetOrCreateUserDirectory(userId);

            string path = Path.Combine(directory, imageName);
            System.IO.File.Delete(path);
        }

        private string GetOrCreateUserDirectory(Guid userId)
        {
            var directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "users", userId.ToString());
            Directory.CreateDirectory(directory);

            return directory;
        }
    }
}
