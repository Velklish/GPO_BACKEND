using GIS_API.DBModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GIS_API.DataRepositories.UserRoles
{
    public class UserRolesRepository : IUserRolesRepository
    {
        private readonly DataContext dataContext;

        public UserRolesRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task AddUserRole(UserRole userRole)
        {
            await this.dataContext.userRoles.AddAsync(userRole);
            await this.dataContext.SaveChangesAsync();
        }

        public async Task AddUserRoles(List<UserRole> userRoles)
        {
            await this.dataContext.userRoles.AddRangeAsync(userRoles);
            await this.dataContext.SaveChangesAsync();
        }

        public void DeleteRelatedToRole(int roleId)
        {
            var objects = this.dataContext.userRoles.Where(x => x.RoleId == roleId);
            this.dataContext.userRoles.RemoveRange(objects);
            this.dataContext.SaveChanges();
        }

        public void DeleteRelatedToUser(int userId)
        {
            var objects = this.dataContext.userRoles.Where(x => x.UserId == userId);
            this.dataContext.userRoles.RemoveRange(objects);
            this.dataContext.SaveChanges();
        }

        public async Task<List<UserRole>> GetUserRoles(int userId)
        {
            return await this.dataContext.userRoles.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
