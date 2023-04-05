using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.Shared.DataTransferObjects.Request
{
    /*public class InitiateTransferRequest
    {
        public int Amount { get; set; }
        public string Source { get; set; } = "balance";
        public string Recipientcode { get; set; }
        public string Currency { get; set; } = "NGN";
        public string Reason { get; set; }
    }
*/
    public class FinalizeTransferRequest
    {
        public string? transfercode { get; set; }
    }


    public class ListTransfersRequest
    {
        public string Status { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int PerPage { get; set; }
        public int Page { get; set; } 
    }

    public class ListRequest
    {
        public int PerPage { get; set; }
        public int Page { get; set; } 
    }
}
