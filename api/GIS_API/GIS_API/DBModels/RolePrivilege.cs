using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GIS_API.DBModels
{
    [Table("role_privilege", Schema = "user_schema")]
    public class RolePrivilege
    {
        [Column("role_privilege_id")]
        public int Id { get; set; }

        [Column("privilege_id")]
        public int PrivilegeId { get; set; }

        [Column("role_id")]
        public int RoleId { get; set; }
    }
}
