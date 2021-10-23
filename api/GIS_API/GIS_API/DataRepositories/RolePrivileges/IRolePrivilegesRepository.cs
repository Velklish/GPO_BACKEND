using GIS_API.DBModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GIS_API.DataRepositories.RolePrivileges
{
    public interface IRolePrivilegesRepository
    {
        public void DeleteRelatedToPrivilege(int privilegeId);

        public void DeleteRelatedToRole(int roleId);

        public Task<List<RolePrivilege>> GetRolePrivileges(int roleId);

        public Task AddRolePrivileges(List<RolePrivilege> rolePrivileges);
    }
}
