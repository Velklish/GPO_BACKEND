using GIS_API.DBModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GIS_API.DataRepositories.Roles
{
    public class RolesRepository : IRolesRepository
    {
        private readonly DataContext dataContext;

        public RolesRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        async Task<Role> IRolesRepository.AddRole(Role role)
        {
            await this.dataContext.roles.AddAsync(role);
            await this.dataContext.SaveChangesAsync();
            return role;
        }

        async Task<Role> IRolesRepository.GetRole(int id)
        {
            return await this.dataContext.roles.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Role>> GetRoles()
        {
            return await this.dataContext.roles.ToListAsync();
        }

        public void DeleteRole(Role role)
        {
            this.dataContext.Remove(role);
            this.dataContext.SaveChanges();
        }
    }
}
