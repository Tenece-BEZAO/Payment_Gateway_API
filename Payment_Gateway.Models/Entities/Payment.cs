namespace Payment_Gateway.Models.Entities
{
    public class Payment : BaseEntity
    {
       
        public DateTime Timestamp { get; set; }
        public bool IsSuccessful { get; set; }
        public Card Card { get; set; }
        public User User { get; set; }

    }

}
