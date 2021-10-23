namespace GIS_API.Managers.EntityType
{
    using System.Collections.Generic;
    using GIS_API.ViewModels;

    public interface IEntityTypeManager
    {
        void AddEntityType(FullEntityTypeViewModel model);

        void DeleteEntityType(LayerNameEntityTypeViewModel model);

        List<EntityTypeViewModel> GetEntitiesWithAttributes();

        FullEntityTypeViewModel GetEntityType(string layerName);

        FullEntityTypeViewModel RedactEntityType(FullEntityTypeViewModel model);
    }
}
