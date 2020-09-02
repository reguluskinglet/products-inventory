using System;
using System.Reflection;
using Authorization.Configuration;
using AutoMapper;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserManagerService.ApplicationServices;
using UserManagerService.Configuration;
using UserManagerService.DAL.Core;
using UserManagerService.DDD;
using UserManagerService.Domain;
using UserManagerService.Domain.Entities;

namespace UserManagerService
{
    public class Startup
    {
        /// <summary>
        /// Конфигурация
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<RouteOptions>(x => x.LowercaseUrls = true);
            services.Configure<ConfigurationManager>(Configuration.GetSection(nameof(ConfigurationManager)));

            //services.AddDataAccessLayer();
            services.AddApplicationLayer();

            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddAutoMapper(typeof(ControllersMappingProfile).Assembly);

            var connectionString = Configuration.GetValue<string>("ConnectionStrings:DatabaseConnection");
            services.AddTransient<DbInitializer>();
            services.AddTransient<IdentityServerConfig>();
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

            services.AddIdentity<User, IdentityRole<Guid>>(options=> {
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 1;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // IdentityServer configuration section.
            var builder = services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
                .AddAspNetIdentity<User>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseNpgsql(connectionString);
                })
                .AddOperationalStore(options => { options.ConfigureDbContext = b => b.UseNpgsql(connectionString); });

            builder.AddDeveloperSigningCredential();

            var assembly = Assembly.GetAssembly(typeof(UserManagementSeed));
            services.AddSingleton(_ => new SessionFactory(connectionString, assembly));
            
            services.AddScoped<UnitOfWork>();
            
            services.AddFluentMigratorCore()
                .ConfigureRunner(x => x
                    .AddPostgres()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(UserManagementSeed).Assembly)
                    .For
                    .Migrations())
                .AddLogging(x => x.AddFluentMigratorConsole());
            
            services.AddHttpClient();

            services.AddMvc(options =>
                {
                    options.EnableEndpointRouting = false;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddLocalApiAuthentication();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseIdentityServer();
            app.UseAuthorizationMiddleware();
            app.UseMvc();
        }
    }
}