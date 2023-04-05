using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.Models.Entities
{
    public class TransferTransaction: BaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Source { get; set; }
        public object? SourceDetails { get; set; }
        public string recipientcode { get; set; }
        public string? Recipient { get; set; }
        public string Currency { get; set; }
        public int Amount { get; set; }
        public bool Status { get; set; }
        public string Reason { get; set; }
        public string transferId { get; set; }


    }
}
