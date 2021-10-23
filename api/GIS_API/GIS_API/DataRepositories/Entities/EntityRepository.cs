using Microsoft.Extensions.Configuration;

namespace GIS_API.DataRepositories.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using GIS_API.DBModels;
    using Npgsql;

    public class EntityRepository : IEntityRepository
    {
        private readonly DataContext dataContext;
        private readonly IConfiguration configuration;

        public EntityRepository(DataContext dataContext, IConfiguration configuration)
        {
            this.dataContext = dataContext;
            this.configuration = configuration;
        }

        public List<Entity> AddEntities(List<Entity> entities)
        {
            this.dataContext.entity.AddRange(entities);
            this.dataContext.SaveChanges();
            return entities;
        }

        public Entity GetEntity(int entityTypeId, int geomId)
        {
            return this.dataContext.entity.First(x => x.GeomId == geomId && x.EntityTypeId == entityTypeId);
        }

        public List<int> GetGeomIds(int entityTypeId)
        {
            string sql3 = @"proc_schema.get_geom_id";

            NpgsqlConnection pgcon = new NpgsqlConnection(configuration.GetConnectionString("DataContext"));
            pgcon.Open();
            NpgsqlCommand pgcom = new NpgsqlCommand(sql3, pgcon);
            pgcom.CommandType = CommandType.StoredProcedure;
            pgcom.Parameters.AddWithValue("entity_type_id_param", entityTypeId);
            NpgsqlDataReader pgreader = pgcom.ExecuteReader();
            var idsList = new List<int>();
            while (pgreader.Read())
            {
                var smth = (int[])pgreader.GetValue(0);
                idsList = smth.ToList();
            }

            return idsList;
        }
    }
}
