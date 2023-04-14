using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Payment_Gateway.BLL.Interfaces;
using Payment_Gateway.DAL.Interfaces;
using Payment_Gateway.Models.Entities;
using Payment_Gateway.Shared.DataTransferObjects.Response;

namespace Payment_Gateway.BLL.Implementation
{
    public class PayoutServiceExtension : IPayoutServiceExtension
    {
        private readonly IRepository<ApplicationUser> _appuserRepo;
        private readonly IRepository<Payout> _payoutRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public PayoutServiceExtension(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _appuserRepo = _unitOfWork.GetRepository<ApplicationUser>();
        }
        public async Task<object> CreatePayout(string userId, TransferResponse response)
        {
            var user = await _appuserRepo.GetSingleByAsync(x => x.Id == userId, include: u => u.Include(x => x.Wallet), tracking: true);
            if(user != null)
            {

                Payout payout = new()
                {
                    WalletId = user.WalletId,
                    reason = response.data.reason,
                    amount = response.data.amount,
                    recipient = response.data.recipient,
                    responsestatus = response.status,
                    status = response.data.status,
                    currency = response.data.currency,
                    reference = response.data.reference,
                    payoutId = response.data.Id,
                };

                var create = _mapper.Map<Payout>(payout);
                var ops = _payoutRepo.AddAsync(create);
                return ops;
            }

            return new InvalidOperationException("Can not create payout");
        }

        public async Task<bool> UpdatePayout(FinalizeTransferResponse Response)
        {
            var payout = await _payoutRepo.GetSingleByAsync(x => x.payoutId == Response.data.Id);

            if (payout.responsestatus == true)
            {
                payout.status = Response.status;
                _payoutRepo.UpdateAsync(payout);
                return true;
            }
            return false;
        }


    }
}
