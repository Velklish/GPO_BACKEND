using System.ComponentModel.DataAnnotations.Schema;

namespace GIS_API.DBModels
{
    [Table("layer", Schema = "geom_schema")]
    public class Layer
    {
        [Column("layer_id")]
        public int Id { get; set; }

        [Column("layer_table_name")]
        public string Name { get; set; }

    }
}
