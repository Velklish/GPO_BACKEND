namespace GIS_API.Managers.Entity
{
    using System.Collections.Generic;
    using GIS_API.DBModels;
    using GIS_API.ViewModels;

    public interface IEntityManager
    {
        public List<int> GetGeomIds(int entityTypeId);

        public List<Entity> AddEntities(List<int> geomIds, int entityTypeId);

        public FullEntityViewModel GetEntity(string layerName, int geomId);

        public FullEntityViewModel RedactEntity(FullEntityViewModel model);
    }
}
