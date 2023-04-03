using Payment_Gateway.Models.Entities;
using Payment_Gateway.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.BLL.Interfaces.IServices
{
    public interface IAdminProfileServices
    {
        Task<AdminProfile> CreateProfile(AdminProfileDto adminProfile);
        void DisplayProfile(int userId);
        Task<AdminProfile> UpdateProfile(AdminProfileDto adminProfile);
        void DeleteProfile(int userId);
    }
}
