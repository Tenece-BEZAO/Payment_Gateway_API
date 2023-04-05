using Microsoft.Extensions.Configuration;
using Payment_Gateway.DAL.Interfaces;
using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payment_Gateway.BLL.Paystack.Interfaces;
using PayStack.Net;
using Payment_Gateway.Shared.DataTransferObjects.Request;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Payment_Gateway.BLL.Paystack.Implementation
{
    public class MakePaymentService : IMakePaymentService
    {
        static IConfiguration _configuration;

        IMapper _mapper;
        public MakePaymentService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public TransactionInitializeResponse ProcessPayment(ProcessPaymentRequest paymentRequest)
        {
            string ApiKey = (string)_configuration.GetSection("Paystack").GetSection("ApiKey").Value;
            PayStackApi payStack = new(secretKey: ApiKey);
            TransactionInitializeRequest transactionInitializeRequest = _mapper.Map<TransactionInitializeRequest>(paymentRequest);
            var result = payStack.Transactions.Initialize(transactionInitializeRequest);
            return result;
        }
        public async Task<TransactionInitializeResponse> ProcessPayment(PaymentRequest paymentRequest)
        {
            var payload = new
            {
                email = paymentRequest.Email,
                amount = paymentRequest.AmountInKobo,
                card = new
                {
                    number = paymentRequest.cardNumber,
                    cvv = paymentRequest.cvv,
                    expiry_month = paymentRequest.expiryMonth,
                    expiry_year = paymentRequest.expiryYear
                }
            };

            string apikey = "sk_test_6c6fc60af0119e14cad8cad7000eb9916014a998";

            HttpClient client = new HttpClient();

            var response = await client.PostAsJsonAsync("https://api.paystack.co/charge", payload);

            string responseContent = await response.Content.ReadAsStringAsync();

            JObject responseObject = JObject.Parse(responseContent);
            //throw new NotImplementedException();




            string ApiKey = (string)_configuration.GetSection("Paystack").GetSection("ApiKey").Value;
            PayStackApi payStack = new(secretKey: ApiKey);
            TransactionInitializeRequest transactionInitializeRequest = _mapper.Map<TransactionInitializeRequest>(paymentRequest);
            var result = payStack.Transactions.Initialize(transactionInitializeRequest);
            return result;
        }

        public Task<bool> VerifyPayment()
        {
            throw new NotImplementedException();
        }
    }
}
