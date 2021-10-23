using GIS_API.DBModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GIS_API.DataRepositories.Maps
{
    public class MapsRepository : IMapsRepository
    {
        private readonly DataContext dataContext;

        public MapsRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<Map> AddMap(Map map)
        {
            await this.dataContext.maps.AddAsync(map);
            await this.dataContext.SaveChangesAsync();
            return map;
        }

        public void DeleteMap(Map map)
        {
            this.dataContext.maps.Remove(map);
            this.dataContext.SaveChanges();
        }

        public async Task<List<Map>> GetAllMaps()
        {
            return await this.dataContext.maps.ToListAsync<Map>();
        }

        public async Task<Map> GetMap(int id)
        {
            return await this.dataContext.maps.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
