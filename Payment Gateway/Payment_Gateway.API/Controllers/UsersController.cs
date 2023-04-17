using Microsoft.AspNetCore.Mvc;
using Payment_Gateway.BLL.Interfaces.IServices;
using Payment_Gateway.Shared.DataTransferObjects;

namespace Payment_Gateway.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly IUserServices _userServices;

        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userServices.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("Get user by Id")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var users = await _userServices.GetUserByIdAsync(id);
            return Ok(users);
        }

        [HttpGet("role/{roleName}")]
        public async Task<IActionResult> GetUsersByRole(string roleName)
        {
            var users = await _userServices.GetUsersByRole(roleName);
            return Ok(users);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteUser(string Id)
        {
            var deletedUser = await _userServices.DeleteUser(Id);
            if (!deletedUser)
            {
                return NotFound("User not found, Input the correct Id");
            }
            return Ok(deletedUser);
        }

        [HttpPut("UpdateUser")]

        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserForUpdateDto userForUpdate)
        {
            var updatedUser = await _userServices.UpdateUser(id, userForUpdate);
            if (!updatedUser)
            {
                return NotFound("User not found, Input the correct Id");
            }
            return Ok(new { message = "user details updated successfully" });
        }
    }
}
