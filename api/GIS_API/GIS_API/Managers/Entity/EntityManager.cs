namespace GIS_API.Managers.Entity
{
    using System.Collections.Generic;
    using System.Linq;
    using GIS_API.DataRepositories.Entities;
    using GIS_API.DataRepositories.EntityTypes;
    using GIS_API.DBModels;
    using GIS_API.Managers.Attribute;
    using GIS_API.Managers.Value;
    using GIS_API.ViewModels;

    public class EntityManager : IEntityManager
    {
        private readonly IEntityRepository entityRepository;
        private readonly IEntityTypeRepository entityTypeRepository;
        private readonly IAttributeManager attributeManager;
        private readonly IValueManager valueManager;

        public EntityManager(
            IEntityRepository entityRepository,
            IEntityTypeRepository entityTypeRepository,
            IAttributeManager attributeManager,
            IValueManager valueManager)
        {
            this.entityRepository = entityRepository;
            this.entityTypeRepository = entityTypeRepository;
            this.attributeManager = attributeManager;
            this.valueManager = valueManager;
        }

        public List<Entity> AddEntities(List<int> geomIds, int entityTypeId)
        {
            var entities = new List<Entity>();
            foreach (var id in geomIds)
            {
                entities.Add(new Entity
                {
                    EntityTypeId = entityTypeId,
                    GeomId = id
                });
            }

            return this.entityRepository.AddEntities(entities);
        }

        public FullEntityViewModel GetEntity(string layerName, int geomId)
        {
            var entityType = this.entityTypeRepository.GetEntityType(layerName);
            var entity = this.entityRepository.GetEntity(entityType.Id, geomId);
            var attributes = this.attributeManager.GetAttributesByEntityTypeId(entityType.Id);
            var attributesDictionary = new Dictionary<string, string>();
            var values = this.valueManager.GetValuesByEntityId(entity.Id);
            foreach (var attribute in attributes)
            {
                attributesDictionary.Add(attribute.Name, values.First(x => x.AttributeId == attribute.Id).ContentValue);
            }

            return new FullEntityViewModel
            {
                Attributes = attributesDictionary,
                GeomId = entity.GeomId,
                Layer = entityType.LayerTableName
            };
        }

        public List<int> GetGeomIds(int entityTypeId)
        {
            return this.entityRepository.GetGeomIds(entityTypeId);
        }

        public FullEntityViewModel RedactEntity(FullEntityViewModel model)
        {
            var entityType = this.entityTypeRepository.GetEntityType(model.Layer);
            var entity = this.entityRepository.GetEntity(entityType.Id, model.GeomId);
            var modifiedValues = this.valueManager.RedactValues(model.Attributes, entity.Id, entityType.Id);
            
            return new FullEntityViewModel
            {
                Attributes = modifiedValues,
                GeomId = model.GeomId,
                Layer = model.Layer
            };
        }
    }
}
