using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.BLL.Interfaces.IServices
{
    public interface IWalletService
    {
        Task<bool> UpdateBlance(string userId, long amount);
        Task<bool> GetBlance(string userId, long amount);
    }
}
