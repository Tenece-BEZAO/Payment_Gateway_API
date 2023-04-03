using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Payment_Gateway.BLL.Interfaces.IServices;
using Payment_Gateway.BLL.LoggerService.Implementation;
using Payment_Gateway.DAL.Interfaces;
using Payment_Gateway.Models.Entities;
using Payment_Gateway.Shared.DataTransferObjects;
using System.Security.Claims;

namespace Payment_Gateway.BLL.Implementation.Services
{
    public class AdminProfileServices : IAdminProfileServices
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<AdminProfile> _adminProfileRepo;
        private readonly IRepository<Admin> _adminRepo;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;

        public AdminProfileServices(IHttpContextAccessor httpContextAccessor, ILoggerManager logger, UserManager<User> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _userManager = userManager;

        }
        public AdminProfileServices(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _adminProfileRepo = _unitOfWork.GetRepository<AdminProfile>();
            _adminRepo = _unitOfWork.GetRepository<Admin>();
        }


        public async Task<AdminProfile> CreateProfile(AdminProfileDto adminProfile)
        {
            try
            {
                _logger.LogInfo("Creating Admin user profile");

                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                {
                    _logger.LogError("User is not authorised");
                    throw new Exception("Only admins are authorized to create admin profile.");
                }

                Admin admin = await _adminRepo.GetSingleByAsync(s => s.UserId == userId);

                if (admin == null)
                {
                    throw new Exception("Admin not found");
                }

                var profile = _mapper.Map<AdminProfile>(adminProfile);
                profile.AdminIdentity = admin.Id;

                return await _adminProfileRepo.AddAsync(profile);
            }

            catch (Exception ex)
            {

                _logger.LogError($"Something went wrong in the {nameof(CreateProfile)} service method {ex}");
                throw;
            }
        }

        public async void DeleteProfile(int userId)
        {
           /* try
            {
                _logger.LogInfo("Deleting Admin user profile");

                var user = await _adminProfileRepo.GetSingleByAsync(u => u.Id == userId);

            }
*/
            throw new NotImplementedException();
        }

        public void DisplayProfile(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<AdminProfile> UpdateProfile(AdminProfileDto adminProfile)
        {
            try
            {
                _logger.LogInfo("Updating Admin user profile");

                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                {
                    _logger.LogError("User is not authorised");
                    throw new Exception("Only admins are authorized to update admin profile.");
                }

                Admin admin = await _adminRepo.GetSingleByAsync(s => s.UserId == userId);

                if (admin == null)
                {
                    throw new Exception("Admin not found");
                }

                AdminProfile profile = await _adminProfileRepo.GetSingleByAsync(p => p.AdminIdentity == admin.Id);

                if (profile == null)
                {
                    throw new Exception("Admin profile not found");
                }

               var updateAdmin = _mapper.Map(adminProfile, profile);

                return await _adminProfileRepo.UpdateAsync(updateAdmin);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(UpdateProfile)} service method {ex}");
                throw;
            }
        }

    }
}
