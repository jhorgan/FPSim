using System;
using System.Linq;
using FPSim.Data.Entity;
using FPSim.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FPSim.Api
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            _logger = logger;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("Default")));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigin",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            // Add Mvc and configure the Json Serializer to ignore self referencing 
            // entities i.e. back references in the data model
            services.AddMvc().AddJsonOptions(
                options => options.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            ApplyDatabaseMigrations(app);
            ApplyDatabaseDefaults(app);

            app.UseCors("AllowAllOrigin");

            app.UseMvc();
        }

        private void ApplyDatabaseMigrations(IApplicationBuilder app)
        {
            _logger.LogInformation("Applying database migrations...");

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.Migrate();
            }

            _logger.LogInformation("Applied database migrations complete successfully");
        }

        private void ApplyDatabaseDefaults(IApplicationBuilder app)
        {
            try
            {
                using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

                    using (var unitOfWork = new UnitOfWork(context))
                    {
                        // Create initial application if required
                        var applicationRepository = unitOfWork.Applications;
                        if (!applicationRepository.GetAll().Any())
                        {
                            applicationRepository.Add(new Application()
                            {
                                Name = "FP Sim Application",
                                IsArchived = false,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            });
                        }

                        // Create initial user if required
                        var userRepository = unitOfWork.Users;
                        if (!userRepository.GetAll().Any())
                        {
                            userRepository.Add(new User()
                            {
                                Name = "Test User",
                                Email = "testuser@lanner.com"
                            });
                        }
                        unitOfWork.Complete();
                    }

                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error applying database defaults.");
            }
        }
    }
}
