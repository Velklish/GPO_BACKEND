using GIS_API.DBModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GIS_API.DataRepositories.RolePrivileges
{
    public class RolePrivilegesRepository : IRolePrivilegesRepository
    {
        private readonly DataContext dataContext;

        public RolePrivilegesRepository (DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public void DeleteRelatedToRole(int roleId)
        {
            var objects = this.dataContext.rolePrivileges.Where(x => x.RoleId == roleId);
            this.dataContext.rolePrivileges.RemoveRange(objects);
            this.dataContext.SaveChanges();
        }

        public void DeleteRelatedToPrivilege(int privilegeId)
        {
            var objects = this.dataContext.rolePrivileges.Where(x => x.PrivilegeId == privilegeId);
            this.dataContext.rolePrivileges.RemoveRange(objects);
            this.dataContext.SaveChanges();
        }

        public async Task<List<RolePrivilege>> GetRolePrivileges(int roleId)
        {  
            return await this.dataContext.rolePrivileges.Where(x => x.RoleId == roleId).ToListAsync();
        }

        public async Task AddRolePrivileges(List<RolePrivilege> rolePrivileges)
        {
            await this.dataContext.AddRangeAsync(rolePrivileges);
        }
    }
}
