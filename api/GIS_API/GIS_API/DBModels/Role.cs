using System.ComponentModel.DataAnnotations.Schema;

namespace GIS_API.DBModels
{
    [Table("role_table", Schema = "user_schema")]
    public class Role
    {
        [Column("role_id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

    }
}
