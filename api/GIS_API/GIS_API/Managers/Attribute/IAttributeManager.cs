namespace GIS_API.Managers.Attribute
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using GIS_API.DBModels;
    using GIS_API.ViewModels;

    public interface IAttributeManager
    {
        public List<EntityAttribute> AddAttributes(List<AttributeViewModel> attributes, int entityTypeId);

        public List<EntityAttribute> GetAttributesByEntityTypeId(int entityTypeId);

        public List<EntityAttribute> RedactAttributes(int entityTypeId, List<AttributeViewModel> attributes);
    }
}
