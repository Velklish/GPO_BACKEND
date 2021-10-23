namespace GIS_API.DataRepositories.Values
{
    using GIS_API.DBModels;
    using System.Collections.Generic;
    using System.Linq;

    public class ValueRepository : IValueRepository
    {
        private readonly DataContext dataContext;

        public ValueRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public List<Value> AddValues(List<Value> values)
        {
            this.dataContext.value.AddRange(values);
            this.dataContext.SaveChanges();
            return values;
        }

        public Value GetValue(int entityId, int attributeId)
        {
            var result = this.dataContext.value.First(x =>
                x.EntityId == entityId && x.AttributeId == attributeId);
            return result;
        }

        public List<Value> GetValues(int entityId)
        {
            var result = this.dataContext.value.Where(x => x.EntityId == entityId).ToList();
            return result;
        }

        public Value RedactValue(Value originValueBd, string newValue)
        {
            originValueBd.ContentValue = newValue;
            this.dataContext.SaveChanges();
            return originValueBd;
        }
    }
}
