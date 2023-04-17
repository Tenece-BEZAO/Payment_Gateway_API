using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Payment_Gateway.BLL.Interfaces;
using Payment_Gateway.BLL.Interfaces.IServices;
using Payment_Gateway.BLL.LoggerService.Implementation;
using Payment_Gateway.DAL.Interfaces;
using Payment_Gateway.Models.Entities;
using Payment_Gateway.Shared.DataTransferObjects;

namespace Payment_Gateway.BLL.Implementation.Services
{
    public sealed class UserServices : IUserServices
    {
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<ApplicationUser> _appuserRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;
        private readonly UserManager<User> _userManager;

        private readonly UserManager<ApplicationUser> _userManagerr;
        private readonly IServiceFactory _serviceFactory;
        private readonly IRepository<ApplicationUser> _userRepos;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IRepository<ApplicationRole> _roleRepo;

        public UserServices(ILoggerManager logger, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _userRepo = _unitOfWork.GetRepository<User>();
            _appuserRepo = _unitOfWork.GetRepository<ApplicationUser>();
        }

        public UserServices(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            _unitOfWork = _serviceFactory.GetService<IUnitOfWork>();
            _userManagerr = _serviceFactory.GetService<UserManager<ApplicationUser>>();
            _userRepos = _unitOfWork.GetRepository<ApplicationUser>();
            _roleManager = _serviceFactory.GetService<RoleManager<ApplicationRole>>();
            _roleRepo = _unitOfWork.GetRepository<ApplicationRole>();
        }


        public async Task<User> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            try
            {
                _logger.LogInfo("Checking if user exist, if not create the user.");
                var existingUser = await _userManager.FindByEmailAsync(userForRegistration.Email.Trim().ToLower());
                if (existingUser != null)
                {
                    throw new InvalidOperationException("Email exists!");
                }

                var user = new User
                {
                    FirstName = userForRegistration.FirstName,
                    LastName = userForRegistration.LastName,
                    UserName = userForRegistration.UserName,
                    Email = userForRegistration.Email,
                    PhoneNumber = userForRegistration.PhoneNumber

                };

                user.EmailConfirmed = true;

                var result = await _userManager.CreateAsync(user, userForRegistration.Password);
                if (!result.Succeeded)
                {

                    string errMsg = string.Join("\n", result.Errors.Select(x => x.Description));
                    throw new InvalidOperationException($"Failed to create user:\n{errMsg}");
                }

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(RegisterUser)} service method {ex}");

                throw;
            }
        }

        public void GetUserProfile()
        {
            throw new NotImplementedException();
        }

        public void UpdateUserProfile()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetSecretKey(string userId)
        {
            var user = await _appuserRepo.GetSingleByAsync(x => x.Id == userId, include: u => u.Include(x => x.ApiKey), tracking: true);
            var apiKey = user.ApiKey.ApiSecretKey;
            if(apiKey != null)
            {
                return apiKey;
            }
            throw new InvalidOperationException("No Api key");
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            return await _userManagerr.FindByIdAsync(id);
        }

        public async Task<bool> UpdateUser(string id, UserForUpdateDto userForUpdate)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new ArgumentException($"User not found");
            }

            user.FirstName = userForUpdate.FirstName;
            user.LastName = userForUpdate.LastName;
            user.UserName = userForUpdate.UserName;
            user.Email = userForUpdate.Email;
            user.PhoneNumber = userForUpdate.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (result == null)
            {
                throw new InvalidOperationException($"Failed to update user detail");
            }
            return true;
        }

        public async Task<bool> DeleteUser(string id)
        {
            var user = await _userManagerr.FindByIdAsync(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var result = await _userManagerr.DeleteAsync(user);
            return true;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
        {
            return await _userManagerr.Users.ToListAsync();
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersByRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                throw new ArgumentException($"Role not found: {roleName}");
            }

            var users = await _userManagerr.GetUsersInRoleAsync(roleName);
            return users; throw new NotImplementedException();
        }

        public Task<ApplicationUser> PatchUser(string userId, JsonPatchDocument<UserForUpdateDto> patchDocument)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if ( user == null )
            {
                return null;
            }

            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}
