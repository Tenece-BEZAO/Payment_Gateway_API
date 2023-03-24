namespace Payment_Gateway.Models.Entities
{
    public class PaymentMethod : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Card> Cards { get; set; }
    }

}
