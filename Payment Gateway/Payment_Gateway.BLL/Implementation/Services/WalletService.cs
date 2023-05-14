using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Payment_Gateway.BLL.Interfaces.IServices;
using Payment_Gateway.DAL.Interfaces;
using Payment_Gateway.Models.Entities;
using Payment_Gateway.Shared.DataTransferObjects;

namespace Payment_Gateway.BLL.Implementation.Services
{
    public class WalletService : IWalletService
    {
        private readonly IRepository<ApplicationUser> _UserRepo;
        private readonly IRepository<Wallet> _WalletRepo;
        public readonly IUnitOfWork _unitOfWork;

        public WalletService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _UserRepo = _unitOfWork.GetRepository<ApplicationUser>();
            _WalletRepo = _unitOfWork.GetRepository<Wallet>();
        }
        public async Task<bool> UpdateBlance(string userId, long amount)
        {
            var user = await _UserRepo.GetSingleByAsync(x => x.Id == userId, include: u => u.Include(x => x.Wallet), tracking: true); 
            var balance =  user.Wallet.Balance;
            var wallet = user.Wallet;

            if(wallet != null)
            {
                var newBalance = balance + amount;
                user.Wallet.Balance = newBalance;
                await _WalletRepo.UpdateAsync(wallet);
                return true;
            }

            return false;
        }
    }
}
