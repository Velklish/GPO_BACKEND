namespace GIS_API.DBModels
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("entity", Schema = "attr_schema")]
    public class Entity
    {
        [Column("entity_id")]
        public int Id { get; set; }

        [Column("entity_type_id")]
        public int EntityTypeId { get; set; }

        [Column("geom_id")]
        public int GeomId { get; set; }
    }
}
