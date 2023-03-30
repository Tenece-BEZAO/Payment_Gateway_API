
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Payment_Gateway.API.Filter;
using Payment_Gateway.BLL.Interfaces;
using Payment_Gateway.Models.Entities;
using Payment_Gateway.Shared.DataTransferObjects;
using System.Data;

namespace Payment_Gateway.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authentication;
        private readonly UserManager<User> _userManager;


        public AuthenticationController(IAuthenticationService authentication, UserManager<User> userManager)
        {
            _authentication = authentication;
            _userManager = userManager;
        }



        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            var response = await _authentication.ValidateUser(user);

            if (!response.Success)
                return BadRequest(response);
            return Ok(new
            {
                Token = await _authentication.CreateToken()
            });
        }
    }
}
