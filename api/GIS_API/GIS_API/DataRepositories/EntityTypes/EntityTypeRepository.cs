using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace GIS_API.DataRepositories.EntityTypes
{
    using GIS_API.DBModels;
    using System.Collections.Generic;
    using System.Linq;

    public class EntityTypeRepository : IEntityTypeRepository
    {
        private readonly DataContext dataContext;
        private readonly IConfiguration configuration;

        public EntityTypeRepository(DataContext dataContext, IConfiguration configuration)
        {
            this.dataContext = dataContext;
            this.configuration = configuration;
        }

        public EntityType AddEntityType(EntityType entityType)
        {
            this.dataContext.entity_type.Add(entityType);
            this.dataContext.SaveChanges();

            return entityType;
        }

        public void CreateEntityTypeView(EntityType entityType)
        {
            string sql3 = @"proc_schema.create_geom_view";
            NpgsqlConnection pgcon = new NpgsqlConnection(configuration.GetConnectionString("DataContext"));
            pgcon.Open();
            NpgsqlCommand pgcom = new NpgsqlCommand(sql3, pgcon);
            pgcom.CommandType = CommandType.StoredProcedure;
            pgcom.Parameters.AddWithValue("entity_type_id_param", entityType.Id);
            pgcom.ExecuteNonQuery();
        }

        public void DeleteEntityType(EntityType entityType)
        {
            this.dataContext.entity_type.Remove(entityType);
            this.dataContext.SaveChanges();
        }

        public EntityType GetEntityType(string layerName)
        {
            var result = this.dataContext.entity_type.FirstOrDefault(x => x.LayerTableName == layerName);
            return result;
        }

        public List<EntityType> GetEntityTypes()
        {
            var result = this.dataContext.Set<EntityType>().ToList();
            return result;
        }

        public EntityType RedactEntityType(EntityType entityType)
        {
            var entity = this.dataContext.entity_type.First(x => x.LayerTableName == entityType.LayerTableName);
            entity.Name = entityType.Name;
            entity.Active = entityType.Active;
            this.dataContext.SaveChanges();
            return entity;
        }
    }
}
