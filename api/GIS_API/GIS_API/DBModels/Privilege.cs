using System.ComponentModel.DataAnnotations.Schema;

namespace GIS_API.DBModels
{
    [Table("privilege", Schema = "user_schema")]
    public class Privilege
    {
        [Column("privilege_id")]
        public int Id { get; set; }

        [Column("object_type")]
        public string ObjectType { get; set; }

        [Column("privilege_type")]
        public string PrivilegeType { get; set; }

        [Column("object_id")]
        public int ObjectId { get; set; }
    }

}
