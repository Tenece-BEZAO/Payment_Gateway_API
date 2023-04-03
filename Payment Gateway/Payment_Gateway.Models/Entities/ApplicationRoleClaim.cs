﻿using Microsoft.AspNetCore.Identity;

namespace Payment_Gateway.Models.Entities
{
    public class ApplicationRoleClaim : IdentityRoleClaim<string>
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public bool Active { get; set; } = true;
        public virtual ApplicationRole Role { get; set; }
    }
}