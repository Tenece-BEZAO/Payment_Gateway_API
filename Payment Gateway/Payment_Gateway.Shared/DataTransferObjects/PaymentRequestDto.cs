namespace Payment_Gateway.BLL.Interfaces
{
    public class PaymentRequestDto
    {
        public decimal Amount { get; set; }
        public string Email { get; set; }
        public string Reference { get; set; }
        public string Currency { get; set; }
        public object Metadata { get; set; }
        public string CallbackUrl { get; set; }
    }

}
