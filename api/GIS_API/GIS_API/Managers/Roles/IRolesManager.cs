using GIS_API.DBModels;
using GIS_API.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GIS_API.Managers.Roles
{
    public interface IRolesManager
    {
        public Task<Role> AddNewRole(RoleViewModel model);

        public Task DeleteRole(int id);

        public Task<List<Role>> GetAllRoles();

        public Task<Role> GetRole(int id);

    }
}
