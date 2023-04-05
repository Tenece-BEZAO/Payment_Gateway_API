using Newtonsoft.Json.Linq;
using Payment_Gateway.Models.Entities;
using Payment_Gateway.Shared.DataTransferObjects.Responses;
using System.Security.Claims;

namespace Payment_Gateway.BLL.Interfaces
{
    public interface IJWTAuthenticator
    {
        Task<JwtToken> GenerateJwtToken(ApplicationUser user, string expires = null, List<Claim> additionalClaims = null);
    }
}
