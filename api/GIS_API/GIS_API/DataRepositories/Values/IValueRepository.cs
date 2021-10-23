namespace GIS_API.DataRepositories.Values
{
    using System.Collections.Generic;
    using GIS_API.DBModels;

    public interface IValueRepository
    {
        public List<Value> AddValues(List<Value> values);

        public List<Value> GetValues(int entityId);

        public Value RedactValue(Value targetValue, string newValue);

        public Value GetValue(int entityId, int attributeId);
    }
}
