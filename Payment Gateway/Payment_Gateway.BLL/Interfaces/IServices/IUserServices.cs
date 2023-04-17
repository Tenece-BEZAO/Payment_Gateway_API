using Microsoft.AspNetCore.JsonPatch;
using Payment_Gateway.Models.Entities;
using Payment_Gateway.Shared.DataTransferObjects;

namespace Payment_Gateway.BLL.Interfaces.IServices
{
    public interface IUserServices
    {
        Task<User> RegisterUser(UserForRegistrationDto userForRegistration);
        void GetUserProfile();
        void UpdateUserProfile();
        //NewChange
        Task<ApplicationUser> GetUserById(string id);
        Task <UserDto> GetUserByIdAsync(string id);
        Task<bool> UpdateUser(string id, UserForUpdateDto userForUpdate);
        Task<bool> DeleteUser(string id);
        Task<IEnumerable<ApplicationUser>> GetAllUsers();
        Task<IEnumerable<ApplicationUser>> GetUsersByRole(string roleName);
        Task<ApplicationUser> PatchUser(string userId, JsonPatchDocument<UserForUpdateDto> patchDocument);

    }
}
