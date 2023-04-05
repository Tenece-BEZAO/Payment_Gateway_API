using Payment_Gateway.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Payment_Gateway.BLL.Implementation
{
    public class PaystackService : IPaystackService
    {
        public Task<PaymentResponseDto> InitializeTransactionAsync(string email, decimal amount, string currency)
        {
            throw new NotImplementedException();
        }

        public Task<Transaction> VerifyTransactionAsync(string reference)
        {
            throw new NotImplementedException();
        }
    }
}
