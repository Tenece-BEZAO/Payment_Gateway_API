using Payment_Gateway.Models.Enums;

namespace Payment_Gateway.Models.Entities
{
    public class Transaction : BaseEntity
    {
        public decimal Amount { get; set; }
        public bool IsSuccessful { get; set; }
        public int UserId { get; set; }
        public string TrxRef { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public User User { get; set; }
    }
}
