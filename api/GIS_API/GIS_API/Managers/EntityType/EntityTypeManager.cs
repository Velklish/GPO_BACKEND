namespace GIS_API.Managers.EntityType
{
    using System;
    using System.Collections.Generic;
    using GIS_API.DataRepositories.EntityTypes;
    using GIS_API.Managers.Entity;
    using GIS_API.Managers.Value;
    using GIS_API.Managers.Attribute;
    using GIS_API.DBModels;
    using GIS_API.ViewModels;

    public class EntityTypeManager : IEntityTypeManager
    {
        private readonly IEntityTypeRepository entityTypeRepository;
        private readonly IAttributeManager attributeManager;
        private readonly IEntityManager entityManager;
        private readonly IValueManager valueManager;

        public EntityTypeManager(
            IEntityTypeRepository entityTypeRepository,
            IAttributeManager attributeManager,
            IEntityManager entityManager,
            IValueManager valueManager)
        {
            this.entityTypeRepository = entityTypeRepository;
            this.attributeManager = attributeManager;
            this.entityManager = entityManager;
            this.valueManager = valueManager;
        }

        public void AddEntityType(FullEntityTypeViewModel model)
        {
            if (entityTypeRepository.GetEntityType(model.Layer) != null)
            {
                throw new Exception("Entity type already exist");
            }
            
            var entityType = new EntityType
            {
                LayerTableName = model.Layer,
                Name = model.Name,
                Active = true
            };
            
            entityType = this.entityTypeRepository.AddEntityType(entityType);

            var attributes = attributeManager.AddAttributes(model.Attributes, entityType.Id);

            var ids = this.entityManager.GetGeomIds(entityType.Id);
            var entities = this.entityManager.AddEntities(ids, entityType.Id);

            foreach (var entity in entities)
            {
                this.valueManager.AddDefaultValues(entity.Id, attributes);
            }

            this.entityTypeRepository.CreateEntityTypeView(entityType);
        }

        public void DeleteEntityType(LayerNameEntityTypeViewModel model)
        {
            var entityType = this.entityTypeRepository.GetEntityType(model.Layer);
            if (entityType == null)
            {
                throw new Exception("This entity type doesnt exist");
            }

            this.entityTypeRepository.DeleteEntityType(entityType);
        }

        public List<EntityTypeViewModel> GetEntitiesWithAttributes()
        {
            var results = new List<EntityTypeViewModel>();
            var entityTypes = this.entityTypeRepository.GetEntityTypes();
            foreach (var entityType in entityTypes)
            {
                if (this.attributeManager.GetAttributesByEntityTypeId(entityType.Id).Count != 0)
                {
                    results.Add(new EntityTypeViewModel
                    {
                        Name = entityType.Name,
                        Layer = entityType.LayerTableName,
                    });
                }
            }

            return results;
        }

        public FullEntityTypeViewModel GetEntityType(string layerName)
        {
            var entityType = this.entityTypeRepository.GetEntityType(layerName);
            if (entityType == null)
            {
                throw new Exception("Entity type is not found");
            }

            var attributes = this.attributeManager.GetAttributesByEntityTypeId(entityType.Id);
            var result = this.BuildModel(entityType, attributes);
            return result;
        }

        public FullEntityTypeViewModel RedactEntityType(FullEntityTypeViewModel model)
        {
            var entityType = this.entityTypeRepository.GetEntityType(model.Layer);
            if (entityType == null)
            {
                throw new Exception("Entity type is not found");
            }

            entityType = this.entityTypeRepository.RedactEntityType(new EntityType
            {
                Active = true,
                Name = model.Name,
                LayerTableName = model.Layer,
            });

            var attributes = this.attributeManager.RedactAttributes(entityType.Id, model.Attributes);
            return this.BuildModel(entityType, attributes);
        }

        public FullEntityTypeViewModel BuildModel(EntityType entityType, List<EntityAttribute> attributes)
        {
            var attributesDictionary = new List<AttributeViewModel>();
            foreach (var attribute in attributes)
            {
                attributesDictionary.Add(new AttributeViewModel
                {
                    Code = attribute.Code,
                    DataType = attribute.ValueType,
                    Name = attribute.Name,
                });
            }

            var result = new FullEntityTypeViewModel
            {
                Name = entityType.Name,
                Layer = entityType.LayerTableName,
                Attributes = attributesDictionary,
            };

            return result;
        }
    }
}
