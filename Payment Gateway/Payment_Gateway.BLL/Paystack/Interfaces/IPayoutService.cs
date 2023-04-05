using Newtonsoft.Json.Linq;
using Payment_Gateway.Shared.DataTransferObjects.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.BLL.Paystack.Interfaces
{
    public interface IPayoutService
    {
        Task<JObject> ResolveAccountNumber(ResolveAccountNumberRequest resolveAccountNumberRequest);
        Task<JObject> ListBanks(string Currency);
        Task<JObject> CreateTransferRecipient(CreateRecipientRequest createRecipientRequest);
        Task<JObject> InitiateTransfer(InitiateTransferRequest initiateTransferRequest);
        Task<JObject> FinilizeTransfer(string transferIdOrCode);

    }
}
