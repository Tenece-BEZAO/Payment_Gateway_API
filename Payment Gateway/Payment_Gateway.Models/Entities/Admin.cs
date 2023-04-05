using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.Models.Entities
{
    public class Admin
    {
        public string Role { get; set; } = "Admin";
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
