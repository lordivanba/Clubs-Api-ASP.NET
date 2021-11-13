using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using clubs_api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using clubs_api.Domain.Interfaces;
using clubs_api.Infrastructure.Repositories;

namespace clubs_api
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "clubs_api", Version = "v1" });
            });
            services.AddDbContext<clubsdbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("test01")));
            /*            services.AddDbContext<clubsdbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("test01")));*/
            services.AddTransient<IClubSqlRepository, ClubSqlRepository>();
            services.AddTransient<ITorneoSqlRepository, TorneoSqlRepository>();
            services.AddTransient<IServicioClubSqlRepository, ServicioClubSqlRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "clubs_api v1"));
            }

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
