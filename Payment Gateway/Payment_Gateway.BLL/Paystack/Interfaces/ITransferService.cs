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
        ResolveAccountNumberResponse ResolveAccountNumber(ResolveAccountNumberRequest request);
        CreateTransferRecipientResponse? CreateTransferRecipient(CreateRecipientRequest transferProcessRequest);
        ListTransfersResponse ListTransferRecipients();
        InitiateTransferResponse InitiateTransfer(InitiateTransferRequest request);
        public FetchTransferResponse FetchTransfer(string transferIdOrCode);
        string ListTransfers();
        public void FinalizeTransfer(FinalizeTransferRequest request);
    }
}
