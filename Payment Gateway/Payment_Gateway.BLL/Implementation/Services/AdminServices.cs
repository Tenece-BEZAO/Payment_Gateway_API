using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Payment_Gateway.BLL.Interfaces.IServices;
using Payment_Gateway.BLL.LoggerService.Implementation;
using Payment_Gateway.DAL.Interfaces;
using Payment_Gateway.Models.Entities;
using Payment_Gateway.Shared.DataTransferObjects;
using Payment_Gateway.Shared.DataTransferObjects.Response;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Payment_Gateway.BLL.Implementation.Services
{
    public class AdminServices : IAdminServices
    {
        private readonly IRepository<Admin> _adminRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;
        private readonly IUserServices _userServices;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly HttpClient _httpClient;
        private string? _ApiKey;

        public AdminServices(IUnitOfWork unitOfWork, UserManager<User> userManager, IUserServices userServices)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _userServices = userServices;
            _adminRepo = _unitOfWork.GetRepository<Admin>();

        }

        public AdminServices(IConfiguration configuration, ILoggerManager logger) 
        { 
            _logger = logger;
            _configuration = configuration;
            _ApiKey = (string?)_configuration?.GetSection("Paystack")?.GetSection("ApiKey")?.Value;
            _httpClient = new HttpClient();
        }

        public async Task<CheckBalanceResponse> CheckBalance()
        {
            _logger.LogInfo("Check Balance");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _ApiKey);

            var url = $"https://api.paystack.co/balance";
            var recipientResponse = await _httpClient.GetAsync(url);

            if (recipientResponse != null)
            {
                var listResponse = await recipientResponse.Content.ReadAsStringAsync();
                var getResponse = JsonConvert.DeserializeObject<CheckBalanceResponse>(listResponse);
                _logger.LogInfo($"Balance!");
                return getResponse;
            }
            throw new InvalidOperationException("Can't get balance");
        }

        public async Task<FetchLedgerResponse> FetchLedger()
        {
            _logger.LogInfo("Check Ledger Balance");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _ApiKey);

            var url = $"https://api.paystack.co/balance/ledger";
            var recipientResponse = await _httpClient.GetAsync(url);

            if (recipientResponse != null)
            {
                var ledgerResponse = await recipientResponse.Content.ReadAsStringAsync();
                var getResponse = JsonConvert.DeserializeObject<FetchLedgerResponse>(ledgerResponse);
                _logger.LogInfo($"Ledger!");
                return getResponse;
            }
            return null;
        }

        public async Task<string> RegisterAdmin(AdminForRegistrationDto adminForRegistration)
        {
            try
            {
                _logger.LogInfo("Creating the Admin as a user first, before assigning the admin role to them and them add them to Admins table.");

                var user = await _userServices.RegisterUser(new UserForRegistrationDto
                {
                    FirstName = adminForRegistration.FirstName,
                    LastName = adminForRegistration.LastName,
                    Email = adminForRegistration.Email,
                    Password = adminForRegistration.Password,
                    UserName = adminForRegistration.UserName
                });

                await _userManager.AddToRoleAsync(user, "Admin");

                var admin = new Admin
                {

                    FirstName = adminForRegistration.FirstName,
                    LastName = adminForRegistration.LastName,
                    UserName = adminForRegistration.UserName,
                    PhoneNumber = adminForRegistration.PhoneNumber,
                    Email = adminForRegistration.Email,
                    UserId = user.Id

                };

                if (_adminRepo == null)
                {
                    throw new Exception("_adminRepo has not been properly initialized.");
                }
                var result = await _adminRepo.AddAsync(admin);

                return $"Registration Successful! You now have access as an administrator!";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(RegisterAdmin)} service method {ex}");

                throw;
            }

        }
    }
}
