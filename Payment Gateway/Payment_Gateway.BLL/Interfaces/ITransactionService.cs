using Payment_Gateway.Models.Entities;

namespace Payment_Gateway.BLL.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAllTransactions();
        Task<Transaction> GetTransactions(int Id);
        Task<IEnumerable<Transaction>> GetTransactionsByDate(DateTime date);


    }

}
