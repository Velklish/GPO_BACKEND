using System.Collections.Generic;
using System.Threading.Tasks;
using GIS_API.DBModels;
using Microsoft.EntityFrameworkCore;

namespace GIS_API.DataRepositories.Layers
{
    public class LayersRepository : ILayersRepository
    {
        private readonly DataContext dataContext;

        public LayersRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        
        public async Task<Layer> GetLayer(string layerName)
        {
            return await this.dataContext.layers.FirstOrDefaultAsync(x => x.Name == layerName);
        }

        public async Task<Layer> GetLayer(int id)
        {
            return await this.dataContext.layers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Layer>> GetLayers()
        {
            return await this.dataContext.layers.ToListAsync();
        }
    }
}
