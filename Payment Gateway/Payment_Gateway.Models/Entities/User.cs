using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Payment_Gateway.Models.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
    }

}
