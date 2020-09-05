using System;
using System.Reflection;
using Authorization.Configuration;
using AutoMapper;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductsFacadeApi.ApplicationServices;
using ProductsFacadeApi.Configuration;
using ProductsFacadeApi.DAL;
using ProductsFacadeApi.DAL.Abstractions;
using ProductsFacadeApi.DDD;
using ProductsFacadeApi.Infrastructure.Options;
using ProductsFacadeApi.Middlewares;
using UserManagerService.Client;
using UserManagerService.Client.Options;

namespace ProductsFacadeApi
{
    public class Startup
    {
        /// <summary>
        /// Configuration.
        /// </summary>
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<UserManagerServiceOptions>(
                Configuration.GetSection("ServicesOptions:UserManagerServiceOptions"));
            services.Configure<RouteOptions>(x => x.LowercaseUrls = true);

            var dbConnectionString = Configuration.GetConnectionString("DatabaseConnection");
            var redisOptions = Configuration.GetSection(nameof(RedisOptions)).Get<RedisOptions>();
            var userManagerServiceOptions = Configuration.GetSection("ServicesOptions:UserManagerServiceOptions")
                .Get<UserManagerServiceOptions>();
            services.AddScoped<UserManagerServiceClient>();

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = userManagerServiceOptions.Url;
                    options.RequireHttpsMetadata = false;
                });

            services.AddDataAccessLayer();
            services.AddApplicationServicesLayer();
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            services.AddCors();

            var assembly = Assembly.GetAssembly(typeof(IVersionInfoRepository));
            services.AddSingleton(_ => new SessionFactory(dbConnectionString, assembly));

            services.AddScoped<UnitOfWork>();

            services.AddFluentMigratorCore()
                .ConfigureRunner(x => x
                    .AddPostgres()
                    .WithGlobalConnectionString(dbConnectionString)
                    .ScanIn(typeof(IVersionInfoRepository).Assembly)
                    .For
                    .Migrations())
                .AddLogging(x => x.AddFluentMigratorConsole());

            services.AddMvc(options => { options.EnableEndpointRouting = false; })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(
                        new Newtonsoft.Json.Converters.StringEnumConverter());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddStackExchangeRedisCache(options => { options.Configuration = redisOptions.Configuration; });

            services.AddHttpClient();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseAuthentication();
            app.UseAuthorizationMiddleware();

            app.UseMiddleware<ProductCacheMiddleware>();

            app.UseMvc();
        }
    }
}