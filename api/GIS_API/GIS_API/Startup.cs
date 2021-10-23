using System;
using System.IO;
using System.Net;
using System.Reflection;
using AutoMapper;
using GIS_API.DataRepositories;
using GIS_API.DataRepositories.Attributes;
using GIS_API.DataRepositories.Entities;
using GIS_API.DataRepositories.EntityTypes;
using GIS_API.DataRepositories.Layers;
using GIS_API.DataRepositories.Maps;
using GIS_API.DataRepositories.Privileges;
using GIS_API.DataRepositories.RolePrivileges;
using GIS_API.DataRepositories.Roles;
using GIS_API.DataRepositories.UserRoles;
using GIS_API.DataRepositories.Users;
using GIS_API.DataRepositories.Values;
using GIS_API.Managers.Attribute;
using GIS_API.Managers.Entity;
using GIS_API.Managers.EntityType;
using GIS_API.Managers.MapManager;
using GIS_API.Managers.Privileges;
using GIS_API.Managers.Roles;
using GIS_API.Managers.Users;
using GIS_API.Managers.Value;
using GIS_API.PrivilegesChecker;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace GIS_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });

            services.AddSingleton(mapperConfig.CreateMapper());
            services.AddCors();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Наш любимый GIS_API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Укажите токен получаемый в методе /login",
                    Name = "token",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddControllers();

            services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DataContext")));

            services.AddScoped<IAuthRepository, AuthRepository>();

            services.AddScoped<IEntityTypeRepository, EntityTypeRepository>();
            services.AddScoped<IAttributeRepository, AttributeRepository>();
            services.AddScoped<IEntityRepository, EntityRepository>();
            services.AddScoped<IValueRepository, ValueRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IPrivilegesRepository, PrivilegesRepository>();
            services.AddScoped<IRolePrivilegesRepository, RolePrivilegesRepository>();
            services.AddScoped<IMapsRepository, MapsRepository>();
            services.AddScoped<IUserRolesRepository, UserRolesRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILayersRepository, LayersRepository>();


            services.AddScoped<IEntityTypeManager, EntityTypeManager>();
            services.AddScoped<IAttributeManager, AttributeManager>();
            services.AddScoped<IEntityManager, EntityManager>();
            services.AddScoped<IValueManager, ValueManager>();
            services.AddScoped<IPrivilegesManager, PrivilegesManager>();
            services.AddScoped<IMapsManager, MapsManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IRolesManager, RolesManager>();

            services.AddScoped<IAccessChecker, AccessChecker>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Наш любимый GIS_API");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        context.Response.AddApplicationError(error.Error.Message);
                        await context.Response.WriteAsync(error.Error.Message);
                    }
                });
            });

            app.UseMiddleware<TokenCheckerMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
