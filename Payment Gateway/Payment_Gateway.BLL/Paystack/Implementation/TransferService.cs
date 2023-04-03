using AutoMapper;
using Microsoft.Extensions.Configuration;
using Payment_Gateway.BLL.Paystack.Interfaces;
using Payment_Gateway.Shared.DataTransferObjects.Request;
using PayStack.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.BLL.Paystack.Implementation
{
    public class TransferService : ITransferService
    {
        private readonly PayStackApi _PayStack;
        static IConfiguration _configuration;
        IMapper _mapper;

        public TransferService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }


        public CreateTransferRecipientResponse? CreateTransferRecipient(TransferProcessRequest transferProcessRequest)
        {
            string? ApiKey = (string?)_configuration.GetSection("Paystack")?.GetSection("ApiKey")?.Value;
            if(ApiKey != null)
            {
                PayStackApi payStack = new(secretKey: ApiKey);
                payStack = _PayStack;
                var api = new PayStackApi("sk_test_your_secret_key_here");


                CreateTransferRecipientRequest transactionInitializeRequest = _mapper.Map<CreateTransferRecipientRequest>(transferProcessRequest);
                var result = payStack.Transfers.Recipients.Create(transactionInitializeRequest);
                return result;
            }
            return null;

        }

        public Task<InitiateTransferResponse> InitiateTransfer(InitiateTransferRequest request)
        {
            InitiateTransferResponse initiateTransferResponse = _mapper.Map<InitiateTransferResponse>(request);
            var result = _PayStack.Post<InitiateTransferResponse, object>("transfer", );
 
            throw new NotImplementedException();
        }

        public Task<object> FetchTransfer()
        {
            throw new NotImplementedException();
        }

        public Task<object> FinalizeTransfer()
        {
            throw new NotImplementedException();
        }

        

        public Task<object> ListTransferRecipients()
        {
            throw new NotImplementedException();
        }

        public Task<object> ListTransfers()
        {
            throw new NotImplementedException();
        }
    }
}
