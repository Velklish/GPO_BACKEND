namespace GIS_API.DataRepositories.EntityTypes
{
    using System.Collections.Generic;
    using GIS_API.DBModels;

    public interface IEntityTypeRepository
    {
        EntityType AddEntityType(EntityType entityType);

        EntityType GetEntityType(string layerName);

        void DeleteEntityType(EntityType entityType);

        List<EntityType> GetEntityTypes();

        EntityType RedactEntityType(EntityType entityType);

        void CreateEntityTypeView(EntityType entityType);
    }
}
