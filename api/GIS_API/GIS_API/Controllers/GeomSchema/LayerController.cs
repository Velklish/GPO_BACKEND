using System.Collections.Generic;
using System.Threading.Tasks;
using GIS_API.DataRepositories.Layers;
using GIS_API.DBModels;
using GIS_API.Enums;
using GIS_API.Managers.Privileges;
using GIS_API.PrivilegesChecker;
using Microsoft.AspNetCore.Mvc;

namespace GIS_API.Controllers.GeomSchema
{
    [Route("[controller]")]
    [ApiController]
    public class LayerController : ControllerBase
    {
        private readonly IPrivilegesManager privilegesManager;
        private readonly ILayersRepository layersRepository;
        private readonly IAccessChecker accessChecker;

        public LayerController(IPrivilegesManager privilegesManager, ILayersRepository layersRepository,
            IAccessChecker accessChecker)
        {
            this.privilegesManager = privilegesManager;
            this.layersRepository = layersRepository;
            this.accessChecker = accessChecker;
        }

        [HttpGet]
        [Route("/layers")]
        public async Task<List<Layer>> GetAllLayers()
        {
            var layers = await this.layersRepository.GetLayers();
            var result = new List<Layer>();
            foreach (var layer in layers)
            {
                if (await this.accessChecker.CheckUserAccessForLayer(this.GetToken(), PrivilegeType.Read, layer.Name))
                {
                    result.Add(layer);
                }
            }
            
            return await Task.FromResult(result);
        }

        [HttpGet]
        [Route("/layer")]
        public async Task<Layer> GetLayer(int id)
        {
            var layer = await this.layersRepository.GetLayer(id);
            return await Task.FromResult(layer);
        }

        private string GetToken()
        {
            return Request.Headers["token"].ToString();
        }
    }
}