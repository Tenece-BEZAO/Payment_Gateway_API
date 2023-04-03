using Payment_Gateway.Models.Entities;
using Payment_Gateway.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.BLL.Interfaces.IServices
{
    public interface IUserProfileService
    {
        Task<CustomerProfile> CreateUserProfile(UserProfileDto userProfile);
        Task<string> DisplayUserProfile(int Id);
        Task<CustomerProfile> UpdateUserProfile(AdminProfileDto userProfile);
        Task<string> DeleteUserProfile(int Id);
    }
}
