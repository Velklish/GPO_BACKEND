using System.ComponentModel.DataAnnotations.Schema;

namespace GIS_API.DBModels
{
    [Table("user_table", Schema = "user_schema")]
    public class User
    {
        [Column("user_id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("admin")]
        public bool IsAdmin { get; set; }
    }
}
