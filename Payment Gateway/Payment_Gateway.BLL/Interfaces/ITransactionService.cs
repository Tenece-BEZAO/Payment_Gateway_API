using Payment_Gateway.Models.Entities;
using Payment_Gateway.Shared.DataTransferObjects;

namespace Payment_Gateway.BLL.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAllTransactions();
        Task<Transaction> GetTransactions(int Id);
        Task<IEnumerable<Transaction>> GetTransactionsByDate(DateTime date);

        Task<TransactionDto> GetTransactionByReference(string reference);



    }

}
