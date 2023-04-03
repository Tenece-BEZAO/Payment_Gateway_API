using Payment_Gateway.Models.Entities;
using Payment_Gateway.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.Shared.DataTransferObjects
{
    public record UserProfileDto: UserForRegistrationDto
    {
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        public string Role { get; set; } = "User";
        public Gender Gender { get; set; }
    }
}
