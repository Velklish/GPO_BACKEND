namespace GIS_API.Managers.Value
{
    using System.Collections.Generic;
    using GIS_API.DBModels;

    public interface IValueManager
    {
        public List<Value> AddDefaultValues(int entityId, List<EntityAttribute> attributes);

        public List<Value> GetValuesByEntityId(int entityId);

        public Dictionary<string, string> RedactValues(Dictionary<string,string> values, int entityId, int entityTypeId);
    }
}
