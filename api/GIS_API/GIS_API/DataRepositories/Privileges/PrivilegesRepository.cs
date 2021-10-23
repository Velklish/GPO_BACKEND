using GIS_API.DBModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GIS_API.DataRepositories.Privileges
{
    public class PrivilegesRepository : IPrivilegesRepository
    {
        private readonly DataContext dataContext;

        public PrivilegesRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<Privilege> AddPrivilege(Privilege privilege)
        {
            await this.dataContext.AddAsync(privilege);
            await this.dataContext.SaveChangesAsync();
            return privilege;
        }

        public void DeletePrivilege(Privilege privilege)
        {
            this.dataContext.privileges.Remove(privilege);
            this.dataContext.SaveChanges();
        }

        public void DeletePrivileges(List<Privilege> privileges)
        {
            this.dataContext.privileges.RemoveRange(privileges);
            this.dataContext.SaveChanges();
        }

        public async Task<List<Privilege>> GetAllPrivileges()
        {
            return await this.dataContext.privileges.ToListAsync();
        }

        public async Task<Privilege> GetPrivilege(int id)
        {
            return await this.dataContext.privileges.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Privilege>> GetPrivilegesForObject(string objectType, int objectId)
        {
            return await this.dataContext.privileges.Where(x => x.ObjectType == objectType && x.ObjectId == objectId).ToListAsync();
        }
    }
}
