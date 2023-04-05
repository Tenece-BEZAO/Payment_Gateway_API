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

        public Task<bool> VerifyPayment()
        {
            throw new NotImplementedException();
        }
    }
}
