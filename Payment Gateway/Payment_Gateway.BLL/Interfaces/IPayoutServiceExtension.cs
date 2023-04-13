using Payment_Gateway.Shared.DataTransferObjects.Response;

namespace Payment_Gateway.BLL.Interfaces
{
    public interface IPayoutServiceExtension
    {
        Task<object> CreatePayout(string userId, TransferResponse response);
        Task<bool> UpdatePayout(FinalizeTransferResponse Response);
    }
}
