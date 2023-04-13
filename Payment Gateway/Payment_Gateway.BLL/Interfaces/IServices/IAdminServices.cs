using Payment_Gateway.Shared.DataTransferObjects;
using Payment_Gateway.Shared.DataTransferObjects.Response;

namespace Payment_Gateway.BLL.Interfaces.IServices
{
    public interface IAdminServices
    {
        Task<string> RegisterAdmin(AdminForRegistrationDto adminForRegistration);
        Task<CheckBalanceResponse> CheckBalance();
        Task<FetchLedgerResponse> FetchLedger();
    }
}
