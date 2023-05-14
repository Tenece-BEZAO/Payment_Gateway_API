using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Payment_Gateway.BLL.Interfaces;
using Payment_Gateway.DAL.Interfaces;
using Payment_Gateway.Models.Entities;
using Payment_Gateway.Shared.DataTransferObjects.Response;

namespace Payment_Gateway.BLL.Implementation
{
    internal class PaymentServiceExtension : IPaymentServiceExtension
    {
        private readonly IRepository<ApplicationUser> _appuserRepo;
        private readonly IRepository<Transaction> _transaactionRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentServiceExtension(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _appuserRepo = _unitOfWork.GetRepository<ApplicationUser>();
        }

        public async Task<object> CreatePayment(string userId, PaymentResponse response)
        {
            var user = await _appuserRepo.GetSingleByAsync(x => x.Id == userId, include: u => u.Include(x => x.Wallet), tracking: true);
            if (user != null)
            {

                Transaction transaction = new()
                {
                    WalletId = user.WalletId,
                    Reference = response.data.reference,
                    Amount = response.data.amount,
                    AccountName = response.data.authorization.account_name,
                    Bank = response.data.authorization.bank,
                    Status = response.data.status,
                    GatewayResponse = response.data.gateway_response,
                    CreatedAt = response.data.created_at,
                    PaidAt = response.data.paid_at,
                    //Email = response.data.Email,
                    Channel = response.data.channel,
                    AuthorizationCode = response.data.authorization.authorization_code,
                    Transactionid = response.data.id,
                    IpAddress = response.data.ip_address,
                    CardType = response.data.authorization.card_type
                };

                var create = _mapper.Map<Transaction>(transaction);
                var ops = _transaactionRepo.AddAsync(create);
                return ops;
            }

            return new InvalidOperationException("Can not create payout");
        }
    }
}
