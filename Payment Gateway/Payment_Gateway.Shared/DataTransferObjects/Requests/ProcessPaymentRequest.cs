using Newtonsoft.Json;
using System;
using Payment_Gateway.Models.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.Shared.DataTransferObjects.Request
{

    public class PaymentRequest
    {
        [Required]
        public string Reference { get; set; }

        [Required]
        public int AmountInKobo { get; set; }
        [Required]
        public string Email { get; set; }

        //[JsonProperty("callback_url")]
        public string CallbackUrl { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; } = "NGN";

        public string Bearer { get; set; }
        public string cardNumber { get; set; }
        public string cvv { get; set; }
        public int expiryMonth { get; set; }
        public int expiryYear { get; set; }
    }
    public class ProcessPaymentRequest 
    {
        //[Required]
        //public string Reference { get; set; }

        //[JsonProperty("amount")]
        //[Required]
        //public int AmountInKobo { get; set; }
        //[Required]
        //public string Email { get; set; }

        //[JsonProperty("callback_url")]
        //public string CallbackUrl { get; set; }

        //[JsonProperty("currency")]
        //public string Currency { get; set; } = "NGN";

        //public string Bearer { get; set; }
        //public string cardNumber { get; set; }
        //public string cvv { get; set; }
        //public int amount { get; set; }
        //public int expiryMonth { get; set; }
        //public int expiryYear { get; set; }









    }


}
