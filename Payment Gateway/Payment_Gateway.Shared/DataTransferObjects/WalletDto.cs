using Payment_Gateway.Models.Entities;
using Payment_Gateway.Models.Enums;
using Payment_Gateway.Models.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.Shared.DataTransferObjects
{
    public class WalletDto
    {
        public string WalletId { get; set; }
        public long Balance { get; set; }
        public string Currency { get; set; }
        public string UserId { get; set; }
    }
}
