using Payment_Gateway.Models.Enums;

namespace Payment_Gateway.Models.Entities
{
    public class Payout : BaseEntity
    {
        public decimal Amount { get; set; }
        public bool IsSuccessful { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
