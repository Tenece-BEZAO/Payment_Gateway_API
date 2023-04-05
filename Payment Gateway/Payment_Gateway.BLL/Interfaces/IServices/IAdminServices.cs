using Payment_Gateway.Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.BLL.Interfaces.IServices
{
    public interface IAdminServices
    {
        Task<string> RegisterAdmin(AdminForRegistrationDto adminForRegistration);
    }
}
