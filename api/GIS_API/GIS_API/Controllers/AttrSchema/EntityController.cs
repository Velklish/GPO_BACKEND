using System.Threading.Tasks;
using GIS_API.Enums;
using GIS_API.Managers.Entity;
using GIS_API.PrivilegesChecker;
using GIS_API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GIS_API.Controllers.AttrSchema
{
    [ApiController]
    [Route("[controller]")]
    public class EntityController : ControllerBase
    {
        private readonly IEntityManager entityManager;
        private readonly IAccessChecker accessChecker;

        public EntityController(IEntityManager entityManager, IAccessChecker accessChecker)
        {
            this.entityManager = entityManager;
            this.accessChecker = accessChecker;
        }

        [HttpGet]
        [Route("/entity")]
        public async Task<ActionResult<FullEntityViewModel>> GetEntityType(string layer, int geomId)
        {
            if (!await this.accessChecker.CheckUserAccessForLayer(GetToken(), PrivilegeType.Read, layer))
            {
                return StatusCode(403);
            }
            
            var result = this.entityManager.GetEntity(layer, geomId);
            return result;
        }

        [HttpPut]
        [Route("/entity")]
        public async Task<ActionResult<FullEntityViewModel>> RedactEntity(FullEntityViewModel model)
        {
            if (!await this.accessChecker.CheckUserAccessForLayer(GetToken(), PrivilegeType.Write, model.Layer))
            {
                return StatusCode(403);
            }
            
            var result = this.entityManager.RedactEntity(model);
            return result;
        }

        private string GetToken()
        {
            return Request.Headers["token"].ToString();
        }
    }
}
