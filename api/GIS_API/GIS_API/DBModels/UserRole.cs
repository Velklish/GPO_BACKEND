using System.ComponentModel.DataAnnotations.Schema;

namespace GIS_API.DBModels
{
    [Table("user_role", Schema = "user_schema")]
    public class UserRole
    {
        [Column("user_role_id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("role_id")]
        public int RoleId { get; set; }
    }
}
