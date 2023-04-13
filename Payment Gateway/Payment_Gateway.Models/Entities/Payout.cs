using Payment_Gateway.Models.Enums;

namespace Payment_Gateway.Models.Entities
{
    public class Payout : BaseEntity
    {
        public decimal amount { get; set; }
        public string reason { get; set; }
        public string recipient { get; set; }
        public string reference { get; set; }
        public string currency { get; set; }
        public string source { get; set; }
        public bool responsestatus { get; set; }
        public string status { get; set; }
        public string WalletId { get; set; }
        public string createdAt { get; set; }
        public string payoutId { get; set; }
        
    }
}
