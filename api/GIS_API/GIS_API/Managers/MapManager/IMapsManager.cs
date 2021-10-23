using GIS_API.DBModels;
using GIS_API.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GIS_API.Managers.MapManager
{
    public interface IMapsManager
    {
        public Task<Map> AddNewMap(MapViewModel model);

        public Task DeleteMap(int id);

        public Task<List<Map>> GetAllMaps();

        public Task<Map> GetMap(int id);
    }
}
