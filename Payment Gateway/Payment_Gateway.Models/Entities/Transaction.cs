using Payment_Gateway.Models.Enums;

namespace Payment_Gateway.Models.Entities
{
    public class Transaction : BaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int Amount { get; set; }
        public string TrxRef { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
    }
}
