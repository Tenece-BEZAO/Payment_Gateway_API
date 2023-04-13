using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Payment_Gateway.BLL.LoggerService.Implementation;
using Payment_Gateway.BLL.Paystack.Interfaces;
using Payment_Gateway.DAL.Interfaces;
using Payment_Gateway.Models.Entities;
using Payment_Gateway.Shared.DataTransferObjects.Request;
using Payment_Gateway.Shared.DataTransferObjects.Response;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace Payment_Gateway.BLL.Paystack.Implementation
{
    public class PayoutService : IPayoutService
    {
        public readonly IRepository<Transaction> _transactionRepo;
        public readonly IRepository<Payout> _payoutRepo;
        private readonly ILoggerManager _logger;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly HttpClient _httpClient;
        private string? _ApiKey;

        public PayoutService(IConfiguration configuration, IUnitOfWork unitOfWork, ILoggerManager logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _payoutRepo = _unitOfWork.GetRepository<Payout>();
            _transactionRepo = unitOfWork.GetRepository<Transaction>();
            _configuration = configuration;
            _ApiKey = (string?)_configuration.GetSection("Paystack")?.GetSection("ApiKey")?.Value;       
            _httpClient = new HttpClient();
        }

       

        public async Task<CreateRecipientResponse> CreateTransferRecipient(CreateRecipientRequest createRecipientRequest)
        {
            _logger.LogInfo("Create Transfer Recipient");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _ApiKey);
            var jsonContent = JsonConvert.SerializeObject(createRecipientRequest);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var recipientResponse = await _httpClient.PostAsync("https://api.paystack.co/transferrecipient", httpContent);

            if (recipientResponse.IsSuccessStatusCode)
            {
                string recipientResponseContent = await recipientResponse.Content.ReadAsStringAsync();
                var getResponse = JsonConvert.DeserializeObject<CreateRecipientResponse>(recipientResponseContent);

                _logger.LogInfo("Transfer recipient created successfully!");
                return getResponse;
            }
            throw new InvalidOperationException("Could not create transfer recipient");
        }


        public async Task<FinalizeTransferResponse> FinilizeTransfer(string transferIdOrCode)
        {
            _logger.LogInfo("Finalize Transfer");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _ApiKey);
            var jsonContent = JsonConvert.SerializeObject(transferIdOrCode);

            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var recipientResponse = await _httpClient.PostAsync("https://api.paystack.co/transfer/finalize_transfer", httpContent);

            //var transaction = await _transactionRepo.GetByAsync(x=>x)

            if (recipientResponse.IsSuccessStatusCode)
            {
                string recipientResponseContent = await recipientResponse.Content.ReadAsStringAsync();
                var getResponse = JsonConvert.DeserializeObject<FinalizeTransferResponse>(recipientResponseContent);
                _logger.LogInfo($"Transfer Done!");
                return getResponse;
            }
            throw new InvalidOperationException("could not finalize transfer");
        }


        public async Task<TransferResponse> InitiateTransfer(InitiateTransferRequest initiateTransferRequest)
        {

            _logger.LogInfo("Initiate Transfer");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _ApiKey);
            var jsonContent = JsonConvert.SerializeObject(initiateTransferRequest);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var recipientResponse = await _httpClient.PostAsync("https://api.paystack.co/transfer", httpContent);

            if (recipientResponse.IsSuccessStatusCode)
            {
                string recipientResponseContent = await recipientResponse.Content.ReadAsStringAsync();
                var getResponse = JsonConvert.DeserializeObject<TransferResponse>(recipientResponseContent);

                _logger.LogInfo($"Transfer Initiated!");
                return getResponse;
            }
            throw new InvalidOperationException("Could not initiate ransfer!");

        }


        public async Task<ListBankResponse> ListBanks(string Currency)
        {
            _logger.LogInfo("Finalize Transfer");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _ApiKey);

            var Url = $"https://api.paystack.co/bank?currency={Currency}";
            var recipientResponse = await _httpClient.GetAsync(Url);

            if (recipientResponse != null)
            {
                var listResponse = await recipientResponse.Content.ReadAsStringAsync();
                var getResponse = JsonConvert.DeserializeObject<ListBankResponse>(listResponse);
                _logger.LogInfo($"Banks Available!");
                return getResponse;
            }
            throw new InvalidOperationException("Could not get list of banks");
        }


        public async Task<ResolveBankResponse> ResolveAccountNumber(ResolveAccountNumberRequest res)
        {
            _logger.LogInfo("Verify Account Number");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _ApiKey);
            var apiUrl = $"https://api.paystack.co/bank/resolve?account_number={res.account_number}&bank_code={res.bank_code}";

            var recipientResponse = await _httpClient.GetAsync(apiUrl);

            if (recipientResponse != null)
            {
                var listResponse = await recipientResponse.Content.ReadAsStringAsync();
                var getResponse = JsonConvert.DeserializeObject<ResolveBankResponse>(listResponse);
                _logger.LogInfo($"account Available!");
                return getResponse;
            }

            throw new InvalidOperationException("Account does not exist!");
        }
    }
}
