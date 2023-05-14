using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Payment_Gateway.BLL.Paystack.Interfaces;
using Payment_Gateway.DAL.Interfaces;
using Payment_Gateway.Models.Entities;
using Payment_Gateway.Models.Enums;
using Payment_Gateway.Shared.DataTransferObjects.Request;
using Payment_Gateway.Shared.DataTransferObjects.Response;
using Payment_Gateway.Shared.DataTransferObjects.Responses;
using PayStack.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Payment_Gateway.BLL.Paystack.Implementation
{
    public class MakePaymentService : IMakePaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Transaction> _transRepo;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private string? _ApiKey;

        IMapper _mapper;
        public MakePaymentService(IConfiguration configuration, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _httpClient = new HttpClient();
            _transRepo = _unitOfWork.GetRepository<Transaction>();
        }

        public async Task<PaymentResponse> MakePayment(PaymentRequest paymentRequest)
        {
            string? ApiKey = _configuration?.GetSection("Paystack").GetSection("ApiKey").Value;
            string? Url = _configuration?.GetSection("Paystack").GetSection("Url").Value;

          
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
            var jasonContent = JsonConvert.SerializeObject(paymentRequest);
            var httpContent = new StringContent(jasonContent, Encoding.UTF8, "application/json");
            var recipientResponse = await _httpClient.PostAsync(Url, httpContent);

            if (recipientResponse.IsSuccessStatusCode)
            { 

                string responseContent = await recipientResponse.Content.ReadAsStringAsync();
                PaymentResponse response = JsonConvert.DeserializeObject<PaymentResponse>(responseContent);
                return response;
            }

             throw new NotImplementedException();
        }


        public Task<bool> VerifyPayment()
        {
            throw new NotImplementedException();
        }

    }
}
