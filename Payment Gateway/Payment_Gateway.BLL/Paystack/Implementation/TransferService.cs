using AutoMapper;
using Azure;
using Azure.Core;
using Microsoft.Extensions.Configuration;
using Payment_Gateway.BLL.LoggerService.Implementation;
using Payment_Gateway.BLL.Paystack.Interfaces;
using Payment_Gateway.DAL.Interfaces;
using Payment_Gateway.Models.Entities;
using Payment_Gateway.Shared.DataTransferObjects.Request;
using PayStack.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.BLL.Paystack.Implementation
{
    public class TransferService : ITransferService
    {
        private readonly IRepository<Transaction> _userRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        private readonly PayStackApi _PayStack;
        public TransferService(IConfiguration configuration, IMapper mapper, ILoggerManager logger)
        {
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
            _userRepo = _unitOfWork.GetRepository<Transaction>();

        }


        public CreateTransferRecipientResponse? CreateTransferRecipient(TransferProcessRequest request)
        {
            string? ApiKey = (string?)_configuration.GetSection("Paystack")?.GetSection("ApiKey")?.Value;
            if(ApiKey != null)
            {
                PayStackApi payStack = new(secretKey: ApiKey);
                payStack = _PayStack;
                /*var url = $"https://api.paystack.co/transferrecipient";*/
                //var api = new PayStackApi("sk_test_your_secret_key_here");

                CreateTransferRecipientRequest transactionInitializeRequest = _mapper.Map<CreateTransferRecipientRequest>(request);
                var result = payStack.Transfers.Recipients.Create(transactionInitializeRequest);
                return result;
            }
            return null;

        }

        public InitiateTransferResponse InitiateTransfer(InitiateTransferRequest request)
        {      
            InitiateTransferResponse initiateTransferResponse = _mapper.Map<InitiateTransferResponse>(request);
            var result = _PayStack.Post<InitiateTransferResponse, object>("transfer", initiateTransferResponse);
            return result;
        }

        public void FinalizeTransfer(CreateTransferRecipientResponse request)
        {
            var otp = _PayStack.Transfers.EnableOtp;
            HttpClient.Content
            _PayStack.Post<IApiResponse, dynamic>("https://api.paystack.co/transfer/finalize_transfer", new
            {
                transfer_code = request.Data.RecipientCode,
                //otp = request.Data.Details..,
            });
        }

        public Task<object> FetchTransfer()
        {
            throw new NotImplementedException();
        }      

        public string? ListTransferRecipient(CreateTransferRecipientResponse request)
        {
            var details = request.Data.Details;
            if(details != null)
            {
                var url = $"https://api.paystack.co/bank/resolve?account_number={details.AccountNumber}&bank_code={details.BankCode}";
                var response = _PayStack.Get<IApiResponse>(url);
                response.Status = true;
                _logger.LogInfo(response.Message);
                return details.AccountName.ToString();

            }
            return "";
        }

        public string? ListTransfers()
        {
            throw new NotImplementedException();
        }

        public TransferOtpResponse ResendOtp(string transferCode, ResendOtpReasons reason) => _api.Post<TransferOtpResponse, dynamic>("transfer/resend_otp", new
        {
            transfer_code = transferCode,
            reason = reason == ResendOtpReasons.ResendOtp ? "resend_otp" : "transfer"
        });

        public TransferOtpResponse DisableOtpBegin() => _api.Post<TransferOtpResponse, dynamic>("transfer/disable_otp", new { });

        public TransferOtpResponse DisableOtpComplete(string otp) => _api.Post<TransferOtpResponse, dynamic>("transfer/disable_otp_finalize",
            new { otp = otp }
        );

        public TransferOtpResponse EnableOtp() => _api.Post<TransferOtpResponse, dynamic>("transfer/enable_otp",
            new { }
        );
    }
}
