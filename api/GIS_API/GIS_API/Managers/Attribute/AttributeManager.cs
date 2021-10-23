namespace GIS_API.Managers.Attribute
{
    using System;
    using System.Collections.Generic;
    using GIS_API.DataRepositories.Attributes;
    using GIS_API.DBModels;
    using GIS_API.ViewModels;

    public class AttributeManager : IAttributeManager
    {
        private readonly IAttributeRepository attributeRepository;

        public AttributeManager(IAttributeRepository attributeRepository)
        {
            this.attributeRepository = attributeRepository;
        }

        public List<EntityAttribute> AddAttributes(List<AttributeViewModel> attributes, int entityTypeid)
        {
            var attributeList = new List<EntityAttribute>();
            try
            {
                attributeList = this.BuildAttributes(attributes, entityTypeid);
            }
            catch
            {
                throw new Exception("Неправильный формат переданных атрибутов");
            }

            return attributeRepository.AddAttributes(attributeList);
        }

        public List<EntityAttribute> GetAttributesByEntityTypeId(int entityTypeId)
        {
            var result = this.attributeRepository.GetAttributesByEntityTypeId(entityTypeId);
            return result;
        }

        public List<EntityAttribute> RedactAttributes(int entityTypeId, List<AttributeViewModel> attributes)
        {
            var attributeList = this.BuildAttributes(attributes, entityTypeId);
            var result = this.attributeRepository.RedactAttributes(entityTypeId, attributeList);
            return result;
        }

        public List<EntityAttribute> BuildAttributes(List<AttributeViewModel> attributes, int entityTypeId)
        {
            var attributeList = new List<EntityAttribute>();
            int i = 1;
            foreach (var attribute in attributes)
            {
                attributeList.Add(new EntityAttribute
                {
                    EntityId = entityTypeId,
                    Name = attribute.Name,
                    ValueType = attribute.DataType,
                    Code = attribute.Code,
                    OrderNumber = i,
                });
                i++;
            }

            return attributeList;
        }
    }
}
