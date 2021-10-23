using System.Collections.Generic;
using System.Threading.Tasks;
using GIS_API.DBModels;

namespace GIS_API.DataRepositories.Layers
{
    public interface ILayersRepository
    {
        Task<Layer> GetLayer(string layerName);

        Task<Layer> GetLayer(int id);

        Task<List<Layer>> GetLayers();
    }
}
