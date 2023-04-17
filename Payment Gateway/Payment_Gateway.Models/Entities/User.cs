using Microsoft.AspNetCore.Identity;

namespace Payment_Gateway.Models.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ApiSecretKey { get; set; }
        public ICollection<Transaction> Transactions { get; set; }

    }

}
