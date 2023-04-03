using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Payment_Gateway.BLL.Interfaces.IServices;
using Payment_Gateway.BLL.LoggerService.Implementation;
using Payment_Gateway.DAL.Interfaces;
using Payment_Gateway.Models.Entities;
using Payment_Gateway.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.BLL.Implementation.Services
{
    public class UserProfileServices : IUserProfileService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly IRepository<CustomerProfile> _userProfileRepo;
        private readonly IRepository<User> _userRepo;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;

        public UserProfileServices(IHttpContextAccessor httpContextAccessor, ILoggerManager logger, UserManager<User> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _userManager = userManager;

        }
        public UserProfileServices(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userRepo = _unitOfWork.GetRepository<User>();
            //_adminRepo = _unitOfWork.GetRepository<Admin>();
        }
        public async Task<Customer> CreateUserProfile(UserProfileDto userProfile)
        {
            try
            {
                _logger.LogInfo("Creating user profile");
                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                {
                    _logger.LogError("User is not authorised");
                    throw new Exception("Only admins are authorized to create user profile.");
                }

                var user = await _userRepo.GetSingleByAsync(s => s.Id == userId);
                if (user == null)
                {
                    throw new Exception("User not found");
                }

                var profile = _mapper.Map<AdminProfile>(adminProfile);
                profile.AdminIdentity = user.Id;

                return await _adminProfileRepo.AddAsync(profile);
            }

            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong in the {nameof(CreateProfile)} service method {ex}");
                throw;
            }
            throw new NotImplementedException();
        }

        public Task<string> DeleteUserProfile(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<string> DisplayUserProfile(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerProfile> UpdateUserProfile(AdminProfileDto userProfile)
        {
            throw new NotImplementedException();
        }
    }
}
