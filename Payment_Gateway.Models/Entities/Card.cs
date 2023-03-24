namespace Payment_Gateway.Models.Entities
{
    public class Card : BaseEntity
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string Cvv { get; set; }
    }

}
