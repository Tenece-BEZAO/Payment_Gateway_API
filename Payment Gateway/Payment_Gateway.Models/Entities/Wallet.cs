namespace Payment_Gateway.Models.Entities
{
    public class Wallet : BaseEntity
    {
        public decimal Balance { get; set; }
        public string Currency { get; set; } = string.Empty;
        public User User { get; set; }  
    }

}
