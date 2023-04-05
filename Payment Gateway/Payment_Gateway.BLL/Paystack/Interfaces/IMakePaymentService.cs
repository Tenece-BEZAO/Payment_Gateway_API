using Payment_Gateway.DAL.Paystack.Request;
using PayStack.Net;
using Payment_Gateway.Shared.DataTransferObjects.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.BLL.Paystack.Interfaces
{
    public interface IMakePaymentService
    {
        TransactionInitializeResponse ProcessPayment(ProcessPaymentRequest paymentRequest);
        Task <TransactionInitializeResponse> ProcessPayment(PaymentRequest paymentRequest);
        Task<bool> VerifyPayment();
    }
}
