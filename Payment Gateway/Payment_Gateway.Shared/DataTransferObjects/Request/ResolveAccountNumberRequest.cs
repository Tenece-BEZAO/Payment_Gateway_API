namespace Payment_Gateway.Shared.DataTransferObjects.Request
{
    public class ResolveAccountNumberRequest
    {
        //[JsonProperty("account_number")]

        public string account_number { get; set; }

        //[JsonProperty("bank_code")]
        public string bank_code { get; set; }
    }

}
