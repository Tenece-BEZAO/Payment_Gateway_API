using Microsoft.AspNetCore.JsonPatch;
using Payment_Gateway.Models.Entities;
using Payment_Gateway.Shared.DataTransferObjects;

namespace Payment_Gateway.BLL.Interfaces.IServices
{
    public interface IUserServices
    {
        //Task<User> RegisterUser(UserForRegistrationDto userForRegistration);
        //void GetUserProfile();
        //void UpdateUserProfile();

        Task<User> GetUserById(string id);
        Task<bool> UpdateUser(string id, UserForUpdateDto userForUpdate);
        Task<bool> DeleteUser(string id);
        Task<IEnumerable<ApplicationUser>> GetAllUsers();
        Task<User> PatchUser(string userId, JsonPatchDocument<UserForUpdateDto> patchDocument);

    }
}
