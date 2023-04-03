using Payment_Gateway.Models.Entities;
using Payment_Gateway.Shared.DataTransferObjects.Request;
using PayStack.Net;

namespace Payment_Gateway.BLL.Paystack.Interfaces
{
    public interface IMakePaymentService
    {
        TransactionInitializeResponse ProcessPayment(DepositPaymentRequest paymentRequest);
        TransactionVerifyResponse VerifyPayment(TransactionVerifyResponse response);
        IEnumerable<Transaction> AllPayments();
    }
}
