namespace GIS_API.DataRepositories.Attributes
{
    using System.Collections.Generic;
    using System.Linq;
    using GIS_API.DBModels;

    public class AttributeRepository : IAttributeRepository
    {
        private readonly DataContext dataContext;

        public AttributeRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public List<EntityAttribute> AddAttributes(List<EntityAttribute> attributes)
        {
            dataContext.attribute.AddRange(attributes);
            dataContext.SaveChanges();
            return attributes;
        }

        public List<EntityAttribute> GetAttributesByEntityTypeId(int entityTypeId)
        {
            return dataContext.attribute.Where(x => x.EntityId == entityTypeId).ToList();
        }

        public List<EntityAttribute> RedactAttributes(int entityTypeId, List<EntityAttribute> attributes)
        {
            var attributesOrigin = dataContext.attribute.Where(x => x.EntityId == entityTypeId);
            this.dataContext.attribute.RemoveRange(attributesOrigin);
            this.dataContext.attribute.AddRange(attributes);
            this.dataContext.SaveChanges();
            return attributes;
        }
    }
}
