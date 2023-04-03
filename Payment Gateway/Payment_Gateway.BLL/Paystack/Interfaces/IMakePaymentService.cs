using Payment_Gateway.Models.Entities;
using Payment_Gateway.Shared.DataTransferObjects.Request;
using PayStack.Net;

namespace Payment_Gateway.BLL.Paystack.Interfaces
{
    public interface IMakePaymentService
    {
        TransactionInitializeResponse ProcessPayment(ProcessPaymentRequest paymentRequest);
        TransactionVerifyResponse VerifyPayment(string reference);
        IEnumerable<Transaction> AllPayments();
    }
}
