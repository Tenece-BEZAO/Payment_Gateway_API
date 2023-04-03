namespace Payment_Gateway.Shared.DataTransferObjects
{
    public class TransactionRequestDto
    {
        public string Id { get; set; }
        public int Amount { get; set; }
        public string TrxRef { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
