using AutoMapper;
using Microsoft.Extensions.Configuration;
using Payment_Gateway.BLL.LoggerService.Implementation;
using Payment_Gateway.BLL.Paystack.Interfaces;
using Payment_Gateway.DAL.Interfaces;
using Payment_Gateway.Models.Entities;
using Payment_Gateway.Shared.DataTransferObjects.Request;
using PayStack.Net;
using System.Drawing;

namespace Payment_Gateway.BLL.Paystack.Implementation
{
    public class TransferService : ITransferService
    {
        private readonly IRepository<Transaction> _userRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        private PayStackApi PayStack;
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
            PayStackApi payStack = new(secretKey: ApiKey);

            if (ApiKey != null)
            {
                CreateTransferRecipientRequest transactionInitializeRequest = _mapper.Map<CreateTransferRecipientRequest>(request);
                var result = payStack.Transfers.Recipients.Create(transactionInitializeRequest);
                return result;
            }
            return null;
        }

        public InitiateTransferResponse InitiateTransfer(InitiateTransferRequest request)
        {
            string? ApiKey = (string?)_configuration.GetSection("Paystack")?.GetSection("ApiKey")?.Value;
            PayStackApi payStack = new(secretKey: ApiKey);
   
            var result = payStack.Transfers.InitiateTransfer(request.amount, request.recipientcode, request.source, request.currency, request.reason );
            
            return result;
        }


        public void FinalizeTransfer(FinalizeTransferRequest request)
        {
            string? ApiKey = (string?)_configuration.GetSection("Paystack")?.GetSection("ApiKey")?.Value;
            PayStackApi payStack = new(secretKey: ApiKey);
            payStack.Transfers.Recipients
            payStack.Transfers.FinalizeTransfer(request.transfercode, EnableOtp().ToString());        
        }



        public FetchTransferResponse FetchTransfer(string transferIdOrCode)
        {
            string? ApiKey = (string?)_configuration.GetSection("Paystack")?.GetSection("ApiKey")?.Value;
            PayStackApi payStack = new(secretKey: ApiKey);

            return payStack.Transfers.FetchTransfer(transferIdOrCode);

        }


        public ListTransfersResponse ListTransferRecipients(ListTransfersRequest request)
        {
            string? ApiKey = (string?)_configuration.GetSection("Paystack")?.GetSection("ApiKey")?.Value;
            PayStackApi payStack = new(secretKey: ApiKey);

            var response = new ListTransfersRequest()
            {
                Status = "success",
                From = "2022-01-01",
                To = "2022-12-31",
                PerPage = 50,
                Page = 1
            };

            return payStack.Transfers.ListTransfers(response.PerPage, response.Page);

        }

        public string? ListTransfers()
        {
            string? ApiKey = (string?)_configuration.GetSection("Paystack")?.GetSection("ApiKey")?.Value;
            PayStackApi payStack = new(secretKey: ApiKey);
            return payStack.Transfers.EnableOtp();
            throw new NotImplementedException();
        }

        public TransferOtpResponse EnableOtp()
        {
            string? ApiKey = (string?)_configuration.GetSection("Paystack")?.GetSection("ApiKey")?.Value;
            PayStackApi payStack = new(secretKey: ApiKey);
            return payStack.Transfers.EnableOtp();
        }
           
        public TransferOtpResponse ResendOtp(string transferCode, ResendOtpReasons reason)
        {
            string? ApiKey = (string?)_configuration.GetSection("Paystack")?.GetSection("ApiKey")?.Value;
            PayStackApi payStack = new(secretKey: ApiKey);

            return payStack.Transfers.ResendOtp(transferCode, reason);
            
        }

        public TransferOtpResponse DisableOtpBegin()
        {
            string? ApiKey = (string?)_configuration.GetSection("Paystack")?.GetSection("ApiKey")?.Value;
            PayStackApi payStack = new(secretKey: ApiKey);

            return payStack.Transfers.DisableOtpBegin();
        }

        public TransferOtpResponse DisableOtpComplete(string otp) => _api.Post<TransferOtpResponse, dynamic>("transfer/disable_otp_finalize",
            new { otp = otp }
        );

        public TransferCheckBalanceResponse CheckBalance() => _api.Get<TransferCheckBalanceResponse>("balance");
    }
}
