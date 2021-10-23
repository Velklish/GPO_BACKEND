namespace GIS_API.Managers.Value
{
    using GIS_API.DBModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GIS_API.DataRepositories.Values;
    using GIS_API.Managers.Attribute;

    public class ValueManager : IValueManager
    {
        private readonly IValueRepository valueRepository;
        private readonly IAttributeManager attributeManager;

        public ValueManager(
            IValueRepository valueRepository,
            IAttributeManager attributeManager)
        {
            this.valueRepository = valueRepository;
            this.attributeManager = attributeManager;
        }

        public List<Value> AddDefaultValues(int entityId, List<EntityAttribute> attributes)
        {
            var valuesList = new List<Value>();
            foreach (var attribute in attributes)
            {
                valuesList.Add(new Value
                {
                    AttributeId = attribute.Id,
                    EntityId = entityId,
                    ContentValue = GetDefaultValue(attribute.ValueType).ToString()
                });
            }

            return this.valueRepository.AddValues(valuesList);
        }

        public List<Value> GetValuesByEntityId(int entityId)
        {
            return this.valueRepository.GetValues(entityId);
        }

        public Dictionary<string,string> RedactValues(Dictionary<string,string> valuesDictionary, int entityId, int entityTypeId)
        {
            var attributes = this.attributeManager.GetAttributesByEntityTypeId(entityTypeId);
            if (attributes.Count == 0)
            {
                throw new Exception("This entity_type doesnt have attributes");
            }

            var valuesList = new List<Value>();
            foreach (var value in valuesDictionary)
            {
                try
                {
                    var attribute = attributes.First(x => x.Name == value.Key);
                    var valueEntity = this.valueRepository.GetValue(entityId, attribute.Id);
                    valuesList.Add(this.valueRepository.RedactValue(valueEntity, valuesDictionary[attribute.Name]));
                }
                catch
                {
                    throw new Exception("Error while redacting values, the reason is: " + value.Key);
                }
            }

            var newValuesDictionary = new Dictionary<string, string>();

            foreach (var value in valuesDictionary)
            {
                var attribute = attributes.First(x => x.Name == value.Key);
                newValuesDictionary.Add(value.Key, valuesList.First(x => x.AttributeId == attribute.Id).ContentValue);
            }

            return newValuesDictionary;
        }

        private dynamic GetDefaultValue(string valueType)
        {
            switch (valueType)
            {
                case "int": return 0;
                case "string": return "Default";
                case "float": return 0.0;
                case "boolean": return true;
            }

            throw new Exception("Invalid attribute data type");
        }

    }
}
