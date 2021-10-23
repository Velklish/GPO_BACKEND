namespace GIS_API.DBModels
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("entity_type", Schema = "attr_schema")]
    public class EntityType
    {
        [Column("entity_type_id")]
        public int Id { get; set; }

        [Column("layer_table_name")]
        public string LayerTableName { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("active")]
        public bool Active { get; set; }
    }
}
