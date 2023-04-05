using Newtonsoft.Json;
using System;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.Shared.DataTransferObjects.Request
{
    public class ProcessPaymentRequest 
    {
        [Required]
        public string Reference { get; set; }

        [JsonProperty("amount")]
        [Required]
        public int AmountInKobo { get; set; }
        [Required]
        public string Email { get; set; }

        [JsonProperty("callback_url")]
        public string CallbackUrl { get; set; }

        [JsonProperty("subaccount")]
        public string SubAccount { get; set; }

        [JsonProperty("transaction_charge")]
        [Required]
        public int TransactionCharge { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; } = "NGN";

        public string Bearer { get; set; }

       
    }
}
