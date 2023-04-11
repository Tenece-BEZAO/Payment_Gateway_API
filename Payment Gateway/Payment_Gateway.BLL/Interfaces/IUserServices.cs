using Payment_Gateway.Models.Entities;
using Payment_Gateway.Shared.DataTransferObjects;

namespace Payment_Gateway.BLL.Interfaces.IServices
{
    public interface IUserServices
    {
        Task<User> RegisterUser(UserForRegistrationDto userForRegistration);
        void GetUserProfile();
        void UpdateUserProfile();

    }
}
