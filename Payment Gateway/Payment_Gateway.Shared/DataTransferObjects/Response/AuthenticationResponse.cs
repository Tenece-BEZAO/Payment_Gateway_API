using Payment_Gateway.Models.Entities;

namespace Payment_Gateway.Shared.DataTransferObjects.Responses
{
    public class AuthenticationResponse
    {
        public JwtToken JwtToken { get; set; }
        public string UserType { get; set; }
        public string FullName { get; set; }
        public IEnumerable<string> MenuItems { get; set; }
        public bool? Birthday { get; set; }
        public bool TwoFactor { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ImpersonatorUsername { get; set; }
        public bool IsImpersonating { get; set; }
    }

    public class JwtToken
    {
        public string Token { get; set; }
        public DateTime Issued { get; set; }
        public DateTime? Expires { get; set; }
    }
}
