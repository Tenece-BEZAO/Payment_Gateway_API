using Payment_Gateway.Models.Enums;

namespace Payment_Gateway.Models.Entities
{
    public class Customer
    {
        public string Address { get; set; }
        public string Role { get; set; } = "Customer";
        public Gender Gender { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public Wallet Wallet { get; set; }
    }
}
