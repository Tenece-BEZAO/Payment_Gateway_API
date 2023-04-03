using Payment_Gateway.Shared.DataTransferObjects.Request;
using PayStack.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.BLL.Paystack.Interfaces
{
    public interface ITransferService
    {
        CreateTransferRecipientResponse? CreateTransferRecipient(TransferProcessRequest transferProcessRequest);
        Task<object> ListTransferRecipients();
        Task<object> InitiateTransfer();
        Task<object> FetchTransfer();
        Task<object> ListTransfers();
        Task<object> FinalizeTransfer();
    }
}
