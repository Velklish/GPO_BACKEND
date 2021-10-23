namespace GIS_API.DataRepositories
{
    using GIS_API.DBModels;
    using Microsoft.EntityFrameworkCore;

    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<EntityType> entity_type { get; set; }

        public DbSet<EntityAttribute> attribute { get; set; }

        public DbSet<Entity> entity { get; set; }

        public DbSet<Value> value { get; set; }

        public DbSet<User> users { get; set; }

        public DbSet<Privilege> privileges { get; set; }

        public DbSet<Role> roles { get; set; }

        public DbSet<RolePrivilege> rolePrivileges { get; set; }

        public DbSet<UserRole> userRoles { get; set; }

        public DbSet<Map> maps { get; set; }
        
        public DbSet<Layer> layers { get; set; }
    }
}
