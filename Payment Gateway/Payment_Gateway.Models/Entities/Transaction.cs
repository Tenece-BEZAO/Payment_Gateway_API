using Payment_Gateway.Models.Enums;

namespace Payment_Gateway.Models.Entities
{
    public class Transaction : BaseEntity
    {
        public decimal Amount { get; set; }
        public bool IsSuccessful { get; set; }
        public int UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
