using Payment_Gateway.Shared.DataTransferObjects.Requests;
using Payment_Gateway.Shared.DataTransferObjects.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.Shared.Interfaces
{
    public interface IAuthenticationService
    {
        Task<string> CreateUser(UserRegistrationRequest request);
        Task<AuthenticationResponse> UserLogin(LoginRequest request);
        //Task<string> ChangePassword(string userId, ChangePasswordRequest request);
        //Task<string> ResetPassword(ResetPasswordRequest request);
        //Task<string> CreateUser(UserRegistrationRequest request);
        //Task<AuthenticationResponse> UserLogin(LoginRequest request);
        //Task<AuthenticationResponse> ConfirmTwoFactorToken(TwoFactorLoginRequest request);
        //Task<string> VerifyUser(VerifyAccountRequest request);
        //Task<string> ChangeEmail(ChangeEmailRequest request);
        //Task ToggleUserActivation(string userId);
        //Task UpdateRecoveryEmail(string userId, string email);
        //Task<UserResponse> GetUser(string userId);
        //Task<AuthenticationResponse> ImpersonateUser(ImpersonationLoginRequest request);
        //Task<ImpersonationLoginResponse> ImpersonationLogin(ImpersonationLoginRequest request);
    }
}
