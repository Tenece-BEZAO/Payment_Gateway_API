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

    public class RoleController : ControllerBase
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

        public RoleController(IRoleService roleservice)
        {
            _roleservice = roleservice;
        }

        [AllowAnonymous]
        [HttpPost("", Name = "Create-Role")]
        [SwaggerOperation(Summary = "Creates role")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Role", Type = typeof(RoleResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Role already exists", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Failed to create role", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> CreateRoleAync(RoleDto request)
        {
            await _roleservice.CreateRoleAync(request);
            return Ok();
        }

    }
}
