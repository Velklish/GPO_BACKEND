namespace GIS_API.DBModels
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("value", Schema = "attr_schema")]
    public class Value
    {
        [Column("value_id")]
        public int Id { get; set; }

        [Column("entity_id")]
        public int EntityId { get; set; }

        [Column("attribute_id")]
        public int AttributeId { get; set; }

        [Column("value")]
        public string ContentValue { get; set; }
    }
}
