using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public UserServices(ILoggerManager logger, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _userRepo = _unitOfWork.GetRepository<User>();
            _appuserRepo = _unitOfWork.GetRepository<ApplicationUser>();
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
    }
}
