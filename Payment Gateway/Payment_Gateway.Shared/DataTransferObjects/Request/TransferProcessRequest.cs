using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.Shared.DataTransferObjects.Request
{
    public class TransferProcessRequest
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
}
