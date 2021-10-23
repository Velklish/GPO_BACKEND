namespace GIS_API.DataRepositories.Entities
{
    using System.Collections.Generic;
    using GIS_API.DBModels;

    public interface IEntityRepository
    {
        public List<int> GetGeomIds(int entityTypeId);

        public List<Entity> AddEntities(List<Entity> entities);

        public Entity GetEntity(int entityTypeId, int geomId);
    }
}
