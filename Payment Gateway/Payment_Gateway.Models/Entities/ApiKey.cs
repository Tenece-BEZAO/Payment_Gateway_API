using Payment_Gateway.Models.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.Models.Entities
{
    public class ApiKey
    {
        [Key]
        public string ApiSecretKey { get; set; } = GenerateApiKey.GenerateAnApiKey();

        [ForeignKey("Id")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
