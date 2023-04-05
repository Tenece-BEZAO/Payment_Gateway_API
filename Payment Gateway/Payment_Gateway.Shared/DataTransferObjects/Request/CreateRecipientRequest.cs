using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.Shared.DataTransferObjects.Request
{
    public class CreateRecipientRequest
    {
        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("description")]
        public string? Description { get; set; }

        [JsonProperty("account_number")]
        public string? AccountNumber { get; set; }

        [JsonProperty("bank_code")]
        public string? BankCode { get; set; }

        [JsonProperty("currency")]
        public string? Currency { get; set; } = "NGN";
    }

    public class ResolveAccountNumberRequest
    {
        [JsonProperty("account_number")]
        public string Accountnumber { get; set; }

        [JsonProperty("bank_code")]
        public string BankCode { get; set; }
    }

    public class InitiateTransferRequest
    {
        [JsonProperty("amount")]
        public int AmountInKobo { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; } = "balance";

        [JsonProperty("recipient_code")]
        public string Recipientcode { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; } = "NGN";

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }= Guid.NewGuid().ToString();
    }
}
