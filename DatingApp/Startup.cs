using System.Collections.Generic;
using System.Linq;
using System.Text;
using DatingApp.Entity;
using DatingApp.Repository;
using DatingApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace DatingApp
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DatingApp")));
            services.AddTransient<Seed>();
            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Values Api", Version = "v1" });
                c.AddSecurityDefinition("Bearer",
                    new ApiKeyScheme
                    {
                        In = "header",
                        Name = "Authorization",
                        Type = "apiKey"
                    });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                    { "Bearer", Enumerable.Empty<string>() },
                });
            });
            services.AddScoped(typeof(IGenericRepository<,,>), typeof(GenericRepository<,,>));
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped<IAuthRepositoryService, AusthService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Seed Seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();


            #region seedingUserTable

            //Uncomment this to re seed our Db 
            //            Seeder.SeedUsers();

            #endregion

            app.UseCors(x => x.AllowAnyHeader().AllowCredentials().AllowAnyMethod().AllowAnyOrigin());
            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Values Api V1");
            });
            app.UseMvc();
        }
    }
}
