﻿using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Payment_Gateway.BLL.Paystack.Interfaces;
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
        static IConfiguration? _configuration;

        IMapper _mapper;
        public MakePaymentService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<object> MakePayment(PaymentRequest paymentRequest)
        {
            string ApiKey = (string)_configuration.GetSection("Paystack").GetSection("ApiKey").Value;
            string Url = (string)_configuration.GetSection("Paystack").GetSection("Url").Value;
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

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
            var jasonContent = JsonConvert.SerializeObject(payload);
            var httpContent = new StringContent(jasonContent, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(Url, httpContent);
            string responseContent = await response.Content.ReadAsStringAsync();
            string reference = JsonConvert.DeserializeObject<dynamic>(responseContent).data.reference;
            string amount = JsonConvert.DeserializeObject<dynamic>(responseContent).data.amount;
            return new PaymentResponse { Reference = reference, Amount = amount };
        }


        public Task<bool> VerifyPayment()
        {
            throw new NotImplementedException();
        }

    }
}
