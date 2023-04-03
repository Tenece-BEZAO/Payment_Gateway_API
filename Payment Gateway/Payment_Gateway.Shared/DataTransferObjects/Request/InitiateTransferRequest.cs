using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.Shared.DataTransferObjects.Request
{
    public class InitiateTransferRequest
    {
        public long amount { get; set; }
        public string source { get; set; } = "balance";
        public string recipientcode { get; set; }
        public string currency { get; set; } = "NGN";
        public string reason { get; set; }
    }

    public class FinalizeTransferRequest
    {
        public string transfercode { get; set; }
        public string otp { get; set; }
    }
}
