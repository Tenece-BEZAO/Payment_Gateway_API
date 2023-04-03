using Microsoft.AspNetCore.Identity;
using Payment_Gateway.BLL.Interfaces.IServices;
using Payment_Gateway.BLL.LoggerService.Implementation;
using Payment_Gateway.DAL.Interfaces;
using Payment_Gateway.Models.Entities;
using Payment_Gateway.Shared.DataTransferObjects;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AutoMapper;

namespace Payment_Gateway.BLL.Implementation.Services
{
    public sealed class UserServices : IUserServices
    {
        private readonly IMapper _mapper;
        private readonly IRepository<User> _userRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;
        private readonly UserManager<User> _userManager;

        public UserServices(ILoggerManager logger, IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _userRepo = _unitOfWork.GetRepository<User>();
            _mapper = mapper;
        }

        public async Task<User> RegisterUser(UserProfileDto userForRegistration)
        {
            try
            {
                _logger.LogInfo("Checking if user exist, if not create the user.");
                var existingUser = await _userManager.FindByEmailAsync(userForRegistration.Email.Trim().ToLower());
                if (existingUser != null)
                {
                    throw new InvalidOperationException("User exists!");
                }

                var user = _mapper.Map<User>(userForRegistration);

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

        public void UpdateUser(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
