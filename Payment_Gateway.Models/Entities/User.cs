namespace Payment_Gateway.Models.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public bool IsAdmin { get; set; }
        public ICollection<Payment> Payments { get; set; }

    }

}
