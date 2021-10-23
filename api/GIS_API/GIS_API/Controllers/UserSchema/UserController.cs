using System.Collections.Generic;
using System.Threading.Tasks;
using GIS_API.Managers.Users;
using Microsoft.AspNetCore.Mvc;

namespace GIS_API.Controllers.UserSchema
{
    [ApiController]
    [AdminPrivilegesChecker]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager userManager;

        public UserController(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        [HttpPut]
        [Route("{userId}/SetRoles")]
        public async Task<IActionResult> SetRoles([FromQuery] int userId, List<int> rolesId)
        {
            await this.userManager.SetRoles(rolesId, userId);
            return Created("User roles were changed", new {rolesId});
        }

        [HttpPost]
        [Route("{userId}/AddRoles")]
        public async Task<IActionResult> AddRole([FromQuery] int userId, List<int> rolesId)
        {
            await this.userManager.AddRoles(rolesId, userId);

            return Created("User roles were added",new { rolesId });
        }

        [HttpDelete]
        [Route("{userId}/RemoveRoles")]
        public async Task<IActionResult> RemoveRole([FromQuery] int userId, List<int> rolesId)
        {
            await this.userManager.AddRoles(rolesId, userId);
            return Created("User roles were added", new { rolesId });
        }
    }
}
