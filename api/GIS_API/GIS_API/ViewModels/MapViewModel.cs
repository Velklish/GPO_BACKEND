namespace GIS_API.ViewModels
{
    public class MapViewModel
    {
        public class Map
        {
            public int? Id { get; set; }

            public string UrlName { get; set; }

            public string Project { get; set; }

            public string Name { get; set; }

            public string Descriprion { get; set; }

            public bool IsMain { get; set; }

            public int GeomId { get; set; }
        }
    }
}
