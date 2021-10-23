namespace GIS_API.DBModels
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("attribute", Schema = "attr_schema")]
    public class EntityAttribute
    {
        [Column("attribute_id")]
        public int Id { get; set; }

        [Column("entity_type_id")]
        public int EntityId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("value_type")]
        public string ValueType { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("order_num")]
        public int OrderNumber { get; set; }

    }
}
