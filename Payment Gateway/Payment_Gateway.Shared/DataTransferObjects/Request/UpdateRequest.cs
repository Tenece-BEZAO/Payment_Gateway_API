using System.ComponentModel.DataAnnotations;

namespace Payment_Gateway.Shared.DataTransferObjects.Requests
{
    public class UpdateRequest
    {
        [Required]
        public string FirstName { get; init; }
        public string LastName { get; init; }

        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; init; }
        [EmailAddress]
        public string Email { get; init; }
        public string? PhoneNumber { get; init; }
    }
}
