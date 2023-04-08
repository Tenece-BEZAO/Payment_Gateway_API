using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.Shared.DataTransferObjects.Response
{
    public class PaymentResponse
    {
        public string Reference { get; set; }
        public string Amount { get; set; }
    }
}
