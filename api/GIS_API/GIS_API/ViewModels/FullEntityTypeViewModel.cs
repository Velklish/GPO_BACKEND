namespace GIS_API.ViewModels
{
    using System.Collections.Generic;

    public class FullEntityTypeViewModel
    {
        public string Layer { get; set; }

        public string Name { get; set; }

        public List<AttributeViewModel> Attributes { get; set; }
    }
}
