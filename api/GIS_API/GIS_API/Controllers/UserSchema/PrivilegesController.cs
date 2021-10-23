using System.Collections.Generic;
using System.Threading.Tasks;
using GIS_API.DBModels;
using GIS_API.Managers.Privileges;
using GIS_API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GIS_API.Controllers.UserSchema
{
    [Route("[controller]")]
    [AdminPrivilegesChecker]
    [ApiController]
    public class PrivilegesController : ControllerBase
    {
        private readonly IPrivilegesManager privilegesManager;

        public PrivilegesController(IPrivilegesManager privilegesManager)
        {
            this.privilegesManager = privilegesManager;
        }

        [HttpGet]
        [Route("/privileges")]
        public async Task<List<Privilege>> GetAllPrivileges()
        {
            var privileges = await this.privilegesManager.GetAllPrivileges();
            return await Task.FromResult(privileges);
        }

        [HttpGet]
        [Route("/privilege")]
        public async Task<Privilege> GetPrivilege(int id)
        {
            var privilege = await this.privilegesManager.GetPrivilege(id);
            return await Task.FromResult(privilege);
        }

        [HttpPost]
        [Route("/privilege")]
        public async Task<Privilege> AddPrivilege(PrivilegeViewModel model)
        {
            return await this.privilegesManager.AddNewPrivilege(model);
        }

        [HttpDelete]
        [Route("/privilege")]
        public async Task<IActionResult> DeletePrivilege(int id)
        {
            await this.privilegesManager.DeletePrivilege(id);
            return Ok("Privilege was deleted");
        }
    }
}
