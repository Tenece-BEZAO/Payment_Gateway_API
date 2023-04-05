using Payment_Gateway.Shared.DataTransferObjects.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment_Gateway.BLL.Interfaces
{
    public interface IRoleService
    {
        Task AddUserToRole(AddUserToRoleRequest request);
        Task CreateRoleAync(RoleDto request);
        Task DeleteRole(RoleDto request);
        Task EditRole(string id, RoleDto request);
        Task RemoveUserFromRole(AddUserToRoleRequest request);
        Task<IEnumerable<string>> GetUserRoles(string userName);
        //Task<PagedResponse<RoleResponse>> GetAllRoles(RoleRequestDto request);
        //Task<IEnumerable<MenuClaimsResponse>> GetRoleClaims(string roleName);
        Task UpdateRoleClaims(UpdateRoleClaimsDto request);
    }
}
