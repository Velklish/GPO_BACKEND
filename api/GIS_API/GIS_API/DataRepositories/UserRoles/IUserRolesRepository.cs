using GIS_API.DBModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GIS_API.DataRepositories.UserRoles
{
    public interface IUserRolesRepository
    {
        public void DeleteRelatedToUser(int userId);

        public void DeleteRelatedToRole(int roleId);

        public Task<List<UserRole>> GetUserRoles(int userId);

        public Task AddUserRole(UserRole userRole);

        public Task AddUserRoles(List<UserRole> userRoles);
    }
}
