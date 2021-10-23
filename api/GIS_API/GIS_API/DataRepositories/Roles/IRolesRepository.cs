using GIS_API.DBModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GIS_API.DataRepositories.Roles
{
    public interface IRolesRepository
    {
        public Task<Role> AddRole(Role role);

        public Task<Role> GetRole(int roleId);

        public Task<List<Role>> GetRoles();

        public void DeleteRole(Role role);

    }
}
