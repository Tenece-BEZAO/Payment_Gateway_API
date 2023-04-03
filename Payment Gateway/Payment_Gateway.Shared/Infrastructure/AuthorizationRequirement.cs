using Microsoft.AspNetCore.Authorization;
namespace Payment_Gateway.Shared.Infrastructure
{
    public class AuthorizationRequirment : IAuthorizationRequirement
    {
        public int Success { get; set; }
    }
}
