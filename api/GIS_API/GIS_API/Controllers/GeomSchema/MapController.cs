using System.Collections.Generic;
using System.Threading.Tasks;
using GIS_API.DBModels;
using GIS_API.Managers.MapManager;
using GIS_API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GIS_API.Controllers.GeomSchema
{
    [Route("[controller]")]
    [ApiController]
    public class MapController : ControllerBase
    {
        private readonly IMapsManager mapsManager;

        public MapController(IMapsManager mapsManager)
        {
            this.mapsManager = mapsManager;
        }

        [HttpGet]
        [Route("/maps")]
        public async Task<List<Map>> GetAllMaps()
        {
            var maps = await this.mapsManager.GetAllMaps();
            return await Task.FromResult(maps);
        }

        [HttpGet]
        [Route("/map")]
        public async Task<Map> GetMap(int id)
        {
            var map = await this.mapsManager.GetMap(id);
            return await Task.FromResult(map);
        }

        [HttpPost]
        [Route("/map")]
        public async Task<Map> AddMap(MapViewModel model)
        {
            return await this.mapsManager.AddNewMap(model);
        }

        [HttpDelete]
        [Route("/map")]
        public async Task<IActionResult> DeleteMap(int id)
        {
            await this.mapsManager.DeleteMap(id);
            return Ok("Map was deleted");
        }
    }
}
