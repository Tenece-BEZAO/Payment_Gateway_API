﻿using Microsoft.AspNetCore.Identity;
using Payment_Gateway.Models.Enums;

namespace Payment_Gateway.Models.Entities   
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string? RecoveryMail { get; set; }
        public DateTime? Birthday { get; set; }
        public UserType UserTypeId { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<ApplicationUserClaim> Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
        public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}