using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Payment_Gateway.BLL.Interfaces.IServices
{
    public interface ITransactionService
    {
        object GetTransaction(string Id);
        object GetTransactions(string Id);
    }
}
