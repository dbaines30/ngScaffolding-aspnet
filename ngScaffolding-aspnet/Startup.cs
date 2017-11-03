using System;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.WebSockets.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ngScaffolding.database;
using ngScaffolding.models.Models;
using ngScaffolding.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace ngScaffolding_aspnet
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddCors();

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;

                    options.ApiName = "ngscaffolding";
                    options.ApiSecret = "secret";
                });

            // Database
            services.AddDbContext<ngScaffoldingContext>(
                options => options.UseSqlServer(_configuration.GetConnectionString("ngScaffolding")));

            // Use a Memory Cache
            services.AddMemoryCache();

            // Configuration Object
            services.AddSingleton<IConfiguration>(_configuration);

            //Repository Pattern Here
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<IRepository<UserPreference>, Repository<UserPreference>>();

            // Services
            services.AddSingleton<IReferenceValuesService, ReferenceValuesService>();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title = "ngScaffolding-aspnet", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            InitializeDatabase(app);

            app.UseAuthentication();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ngScaffolding-aspnet API V1");
            });

            app.UseCors(builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseMvc();
            ;
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ngScaffoldingContext>();
                context.Database.Migrate();
            }
        }
    }
}
