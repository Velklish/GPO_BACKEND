namespace GIS_API.DataRepositories.Attributes
{
    using GIS_API.DBModels;
    using System.Collections.Generic;
    public interface IAttributeRepository
    {
        public List<EntityAttribute> AddAttributes(List<EntityAttribute> attributes);

        public List<EntityAttribute> GetAttributesByEntityTypeId(int entityTypeId);

        public List<EntityAttribute> RedactAttributes(int entityTypeId, List<EntityAttribute> attributes);
    }
}
