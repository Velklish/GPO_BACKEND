using System.Collections.Generic;
using System.Threading.Tasks;
using GIS_API.DBModels;
using GIS_API.Managers.Roles;
using GIS_API.PrivilegesChecker;
using GIS_API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GIS_API.Controllers.UserSchema
{
    [ApiController]
    [AdminPrivilegesChecker]
    [Route("[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRolesManager rolesManager;
        private readonly IAccessChecker accessChecker;

        public RolesController(IRolesManager rolesManager, IAccessChecker accessChecker)
        {
            this.rolesManager = rolesManager;
            this.accessChecker = accessChecker;
        }

        [HttpGet]
        [Route("/Roles")]
        public async Task<ActionResult<List<Role>>> GetAllRoles()
        {
            var roles = await this.rolesManager.GetAllRoles();
            return await Task.FromResult(roles);
        }

        [HttpGet]
        [Route("/Role")]
        public async Task<Role> GetRole(int id)
        {
            var role = await this.rolesManager.GetRole(id);
            return await Task.FromResult(role);
        }

        [HttpPost]
        [Route("/Role")]
        public async Task<Role> AddRole(RoleViewModel model)
        {
            return await this.rolesManager.AddNewRole(model);
        }

        [HttpDelete]
        [Route("/Role")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            await this.rolesManager.DeleteRole(id);
            return Ok("Role was deleted");
        }
    }
}