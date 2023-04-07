using System.ComponentModel.DataAnnotations;

namespace Payment_Gateway.Shared.DataTransferObjects
{
    public record AdminProfileDto : ApplicationUserForRegistrationDto
    {

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; init; }
    }

}
