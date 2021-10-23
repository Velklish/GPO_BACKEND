using System.ComponentModel.DataAnnotations.Schema;

namespace GIS_API.DBModels
{
    [Table("map", Schema = "geom_schema")]
    public class Map
    {
        [Column("map_id")]
        public int Id { get; set; }

        [Column("url_name")]
        public string UrlName { get; set; }

        [Column("project")]
        public string Project { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Descriprion { get; set; }

        [Column("main")]
        public bool IsMain { get; set; }

        [Column("geom_id")]
        public int GeomId { get; set; }
    }
}
