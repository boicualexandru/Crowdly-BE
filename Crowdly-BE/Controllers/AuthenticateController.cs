using AutoMapper;
using Crowdly_BE.Authentication;
using Microsoft.AspNetCore.Mvc;
using Services.Authentication;
using System.Linq;
using System.Threading.Tasks;

namespace Crowdly_BE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public AuthenticateController(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginModel model)
        {
            var loginResponse = await _authenticationService.LoginAsync(_mapper.Map<Services.Authentication.Models.LoginModel>(model));

            if (loginResponse.ErrorMessages.Any())
                return BadRequest(loginResponse.ErrorMessages);

            return _mapper.Map<LoginResponse>(loginResponse);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var errorMessages = await _authenticationService.RegisterAsync(_mapper.Map<Services.Authentication.Models.RegisterModel>(model));

            if (errorMessages.Any())
                return BadRequest(errorMessages);

            return Ok();
        }
    }
}
