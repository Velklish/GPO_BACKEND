using GIS_API.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GIS_API.DataRepositories.Maps
{
    public interface IMapsRepository
    {
        public Task<Map> AddMap(Map Map);

        public Task<Map> GetMap(int id);

        public Task<List<Map>> GetAllMaps();

        public void DeleteMap(Map map);
    }
}
