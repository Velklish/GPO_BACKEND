using System.Collections.Generic;
using System.Threading.Tasks;
using GIS_API.Enums;
using GIS_API.Managers.EntityType;
using GIS_API.PrivilegesChecker;
using GIS_API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GIS_API.Controllers.AttrSchema
{
    [ApiController]
    [Route("[controller]")]
    public class EntityTypeController : ControllerBase
    {
        private readonly IEntityTypeManager entityTypeManager;
        private readonly IAccessChecker accessChecker;

        public EntityTypeController(IEntityTypeManager entityTypeManager, IAccessChecker accessChecker)
        {
            this.entityTypeManager = entityTypeManager;
            this.accessChecker = accessChecker;
        }

        [HttpPost]
        [Route("/entity_type")]
        public async Task<ActionResult<FullEntityTypeViewModel>> AddEntityType(FullEntityTypeViewModel model)
        {
            if (!await this.accessChecker.CheckUserAccessForLayer(GetToken(), PrivilegeType.Write, model.Layer))
            {
                return StatusCode(403);
            }
            
            this.entityTypeManager.AddEntityType(model);
            return model;
        }
        
        [HttpPut]
        [Route("/entity_type")]
        public async Task<ActionResult<FullEntityTypeViewModel>> PutEntityType(FullEntityTypeViewModel model)
        {
            if (!await this.accessChecker.CheckUserAccessForLayer(GetToken(), PrivilegeType.Write, model.Layer))
            {
                return StatusCode(403);
            }
            
            this.entityTypeManager.RedactEntityType(model);
            return model;
        }

        [HttpDelete]
        [Route("/entity_type")]
        public async Task<IActionResult> DeleteEntityType(LayerNameEntityTypeViewModel model)
        {
            if (!await this.accessChecker.CheckUserAccessForLayer(GetToken(), PrivilegeType.Write, model.Layer))
            {
                return StatusCode(403);
            }
            
            this.entityTypeManager.DeleteEntityType(model);
            return Ok();
        }

        [HttpGet]
        [Route("/entity_type")]
        public async Task<ActionResult<FullEntityTypeViewModel>> GetEntityType(string layer)
        {
            if (!await this.accessChecker.CheckUserAccessForLayer(GetToken(), PrivilegeType.Read, layer))
            {
                return StatusCode(403);
            }
            
            var result = this.entityTypeManager.GetEntityType(layer);
            return result;
        }

        [HttpGet]
        [Route("/entity_types")]
        public async Task<ActionResult<List<EntityTypeViewModel>>> GetEntityTypesWithAttributes()
        {
            if (!await this.accessChecker.CheckUserAccessForLayer(GetToken(), PrivilegeType.Read, "railway_line"))
            {
                return StatusCode(403);
            }
            
            var result = this.entityTypeManager.GetEntitiesWithAttributes();
            return Ok(result);
        }
        
        private string GetToken()
        {
            return Request.Headers["token"].ToString();
        }
    }
}
