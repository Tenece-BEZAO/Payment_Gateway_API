using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Payment_Gateway.DAL.Interfaces;
using Payment_Gateway.Models.Entities;
using Payment_Gateway.Shared.DataTransferObjects.Requests;
using Payment_Gateway.Shared.DataTransferObjects.Responses;
using Payment_Gateway.BLL.Implementation;
using Payment_Gateway.BLL.Infrastructure;
using Payment_Gateway.BLL.Interfaces;
using Swashbuckle.AspNetCore.Annotations;


namespace Payment_Gateway.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Authorization")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly IRoleService _roleservice;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IServiceFactory _serviceFactory;
        private readonly IMapper _mapper;
        private readonly IRepository<ApplicationRole> _roleRepo;
        private readonly IRepository<ApplicationRoleClaim> _roleClaimRepo;

        private readonly IUnitOfWork _unitOfWork;
        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("", Name = "Create-New-User")]
        [SwaggerOperation(Summary = "Creates user")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "UserId of created user", Type = typeof(AuthenticationResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "User with provided email already exists", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Failed to create user", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> CreateUser(UserRegistrationRequest request)
        {
            string response = await _authService.CreateUser(request);
            return Ok(response);
        }


        [AllowAnonymous]
        [HttpPost("login", Name = "Login")]
        [SwaggerOperation(Summary = "Authenticates user")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "returns user Id", Type = typeof(AuthenticationResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Invalid username or password", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<ActionResult<AuthenticationResponse>> Login(LoginRequest request)
        {
            AuthenticationResponse response = await _authService.UserLogin(request);
            return Ok(response);
        }


        ////public RoleResponse GetResponse(RoleDto request)
        ////{
        ////    return await _roleservice.CreateRole(request);
        ////}

        //[AllowAnonymous]
        //[HttpPost("addRole", Name = "Create Role")]
        //[SwaggerOperation(Summary = "Create Role")]
        //[SwaggerResponse(StatusCodes.Status200OK, Description = "returns user role", Type = typeof(RoleResponse))]
        //[SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Invalid role", Type = typeof(ErrorResponse))]
        //[SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        //public async Task<IActionResult> CreateRole(RoleDto request)
        //{
        //    ApplicationRole role = await _roleManager.FindByNameAsync(request.Name.Trim().ToLower());

        //    if (role != null)
        //        throw new InvalidOperationException($"Role with name {request.Name} already exist");

        //    ApplicationRole roleToCreate = new ApplicationRole();

        //    await _roleManager.CreateAsync(roleToCreate);
        //    return Ok();
        //}
    }
}
