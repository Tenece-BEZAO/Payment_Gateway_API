using Payment_Gateway.Models.Enums;
using Payment_Gateway.Models.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payment_Gateway.Models.Entities
{
    public class Wallet : BaseEntity
    {
        [Key]
        public string WalletId { get; set; } = AccountNumberGenerator.GenerateRandomNumber();
        public long Balance { get; set; }
        public Currency Currency { get; set; } = Currency.NGN;

        [ForeignKey("Id")]
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }  
    }

}
