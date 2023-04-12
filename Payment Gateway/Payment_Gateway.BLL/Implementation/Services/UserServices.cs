using Microsoft.AspNetCore.Identity;
using Payment_Gateway.BLL.Interfaces.IServices;
using Payment_Gateway.BLL.LoggerService.Implementation;
using Payment_Gateway.DAL.Interfaces;
using Payment_Gateway.Models.Entities;
using Payment_Gateway.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Payment_Gateway.BLL.Interfaces;

namespace Payment_Gateway.BLL.Implementation.Services
{
    public class UserServices : IUserServices
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IServiceFactory _serviceFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;
        private readonly IRepository<ApplicationUser> _userRepo;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IRepository<ApplicationRole> _roleRepo;
        //private readonly UserManager<User> _userManager;

        public UserServices(IServiceFactory serviceFactory, ILoggerManager logger)
        {
            _logger = logger;
            _serviceFactory = serviceFactory;
            _unitOfWork = _serviceFactory.GetService<IUnitOfWork>();
            _userManager = _serviceFactory.GetService<UserManager<ApplicationUser>>();
            _userRepo = _unitOfWork.GetRepository<ApplicationUser>();
            _roleManager =_serviceFactory.GetService<RoleManager<ApplicationRole>>();
            _roleRepo = _unitOfWork.GetRepository<ApplicationRole>();




            //_roleManager = _serviceFactory.GetService<RoleManager<ApplicationRole>>();
            //_roleRepo = _unitOfWork.GetRepository<ApplicationRole>();
        }
        //public UserServices(ILoggerManager logger, IUnitOfWork unitOfWork, UserManager<User> userManager)
        //{
        //    _logger = logger;
        //    _unitOfWork = unitOfWork;
        //    _userManager = userManager;
        //    //_userRepo = _unitOfWork.GetRepository<ApplicationUser>();
        //}

        public async Task<bool> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var result = await _userManager.DeleteAsync(user);
            return true;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersByRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                throw new ArgumentException($"Role not found: {roleName}");
            }

            var users = await _userManager.GetUsersInRoleAsync(roleName);
            return users;
        }

        public Task<ApplicationUser> PatchUser(string userId, JsonPatchDocument<UserForUpdateDto> patchDocument)
        {
            throw new NotImplementedException();
        }

        //public async Task<User> RegisterUser(UserForRegistrationDto userForRegistration)
        //{
        //    try
        //    {
        //        _logger.LogInfo("Checking if user exist, if not create the user.");
        //        var existingUser = await _userManager.FindByEmailAsync(userForRegistration.Email.Trim().ToLower());
        //        if (existingUser != null)
        //        {
        //            throw new InvalidOperationException("Email exists!");
        //        }

        //        var user = new User
        //        {
        //            FirstName = userForRegistration.FirstName,
        //            LastName = userForRegistration.LastName,
        //            UserName = userForRegistration.UserName,
        //            Email = userForRegistration.Email,
        //            PhoneNumber = userForRegistration.PhoneNumber
        //        };

        //        user.EmailConfirmed = true;

        //        var result = await _userManager.CreateAsync(user, userForRegistration.Password);
        //        if (!result.Succeeded)
        //        {

        //            string errMsg = string.Join("\n", result.Errors.Select(x => x.Description));

        //            throw new InvalidOperationException($"Failed to create user:\n{errMsg}");
        //        }

        //        return user;

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Something went wrong in the {nameof(RegisterUser)} service method {ex}");

        //        throw;
        //    }
        //}

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



        //public void GetUserProfile()
        //{
        //    throw new NotImplementedException();
        //}

        //public void UpdateUserProfile()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
