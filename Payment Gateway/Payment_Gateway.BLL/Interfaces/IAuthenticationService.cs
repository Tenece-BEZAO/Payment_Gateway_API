using Microsoft.AspNetCore.Identity;
using Payment_Gateway.Shared.DataTransferObjects;
using Payment_Gateway.Shared.DataTransferObjects.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.BLL.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse<string>> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<string> CreateToken();
        //Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration);
    }

}
