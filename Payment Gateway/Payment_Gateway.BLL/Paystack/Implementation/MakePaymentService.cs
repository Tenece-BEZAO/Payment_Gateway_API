using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Payment_Gateway.BLL.LoggerService.Implementation;
using Payment_Gateway.BLL.Paystack.Interfaces;
using Payment_Gateway.DAL.Interfaces;
using Payment_Gateway.Models.Entities;
using Payment_Gateway.Shared.DataTransferObjects.Request;
using PayStack.Net;

namespace Payment_Gateway.BLL.Paystack.Implementation
{
    public class MakePaymentService : IMakePaymentService
    {
        private readonly IRepository<Transaction> _TransactionRepo;
        private readonly IRepository<Wallet> _walletRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private static IConfiguration _configuration;

        private PayStackApi Paystack { get; set; }

        public MakePaymentService(IConfiguration configuration, IMapper mapper, ILoggerManager logger)
        {
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
            _TransactionRepo = _unitOfWork.GetRepository<Transaction>();
            _walletRepo = _unitOfWork.GetRepository<Wallet>();
        }

        public TransactionInitializeResponse ProcessPayment(ProcessPaymentRequest deposit)
        {
            _logger.LogInfo("Make Payment");

            string? ApiKey = (string?)_configuration.GetSection("Paystack")?.GetSection("ApiKey")?.Value;
            PayStackApi payStack = new(secretKey: ApiKey);
            Paystack = payStack;

            TransactionInitializeRequest request = new()
            {
                AmountInKobo = deposit.AmountInKobo * 100,
                Email = deposit.Email,
                Currency = "NGN",
                Bearer = deposit.Bearer,
                Reference = deposit.Reference,
                CallbackUrl = deposit.CallbackUrl,
            };

            var result = payStack.Transactions.Initialize(request);

            if (result.Status)
            {
                var createTrans = _mapper.Map<Transaction>(deposit);
                _logger.LogInfo("Transaction record created");
                _TransactionRepo.AddAsync(createTrans);
                return result;
            }
            return result;
        }

        public ResolveCardBinResponse ResolveCardBin(string cardBin)
        {
            _logger.LogInfo("Verify Recipient Account details");

            string? ApiKey = (string?)_configuration.GetSection("Paystack")?.GetSection("ApiKey")?.Value;
            PayStackApi payStack = new(secretKey: ApiKey);
            if (payStack != null)
            {
                var result = payStack.Miscellaneous.ResolveCardBin(cardBin);
                return result;
            }
            _logger.LogError("ApiKey is not valid");
            return new ResolveCardBinResponse();
        }

        public ListBanksResponse ListBanks(ListRequest list)
        {
            _logger.LogInfo("Verify Recipient Account details");

            string? ApiKey = (string?)_configuration.GetSection("Paystack")?.GetSection("ApiKey")?.Value;
            PayStackApi payStack = new(secretKey: ApiKey);

            if (payStack != null)
            {             
                var result = payStack.Miscellaneous.ListBanks(list.PerPage, list.Page);
                return result;
            }
            _logger.LogError("ApiKey is not valid");
            return new ListBanksResponse();
        }

        public TransactionVerifyResponse VerifyPayment(string reference)
        {
            TransactionVerifyResponse response = Paystack.Transactions.Verify(reference);
            if (response.Status && response.Data.Status == "success")
            {
                var transaction = _TransactionRepo.GetBy(x => x.TrxRef == reference).FirstOrDefault();
                if (transaction != null)
                {
                    transaction.Status = true;
                    //var updateref = _mapper.Map<Transaction>(transaction);
                    _TransactionRepo.UpdateAsync(transaction);
                    _logger.LogInfo("Transaction record updated");
                    return response;
                }
            }
            _logger.LogError("Transaction Verification Failed");
            return response;
        }


        public IEnumerable<Transaction> AllPayments()
        {
            var transactions = _TransactionRepo.GetAll();
            return transactions.ToList();
        }

        /*public Wallet UpdateWallet(string walletId, long amount)
        {
            var wallet = _walletRepo.GetSingleByAsync(x => x.WalletId == walletId, include: x => x.Include(x => x.Customer), tracking: true);
            if (wallet != null)
            {
                Wallet updateUallet = new()
                {
                    WalletId = walletId,
                    Balance = wallet.Result.Balance + amount,
                };

            }
        }*/
    }
}
