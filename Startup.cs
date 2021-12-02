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
using clubs_api.Application.Services;
using Microsoft.AspNetCore.Http;
using FluentValidation;
using clubs_api.Domain.Dtos.Requests;
using clubs_api.Infrastructure.Validators;
using clubs_api.Domain.Entities;

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
                options.UseSqlServer(Configuration.GetConnectionString("ClubsDb")));
            services.AddTransient<IClubSqlRepository, ClubSqlRepository>();
            services.AddTransient<ITorneoSqlRepository, TorneoSqlRepository>();
            services.AddTransient<IServicioClubSqlRepository, ServicioClubSqlRepository>();
            services.AddTransient<IParticipanteTorneoSqlRepository, ParticipanteTorneoSqlRepository>();

            services.AddScoped<IClubService, ClubService>();
            services.AddScoped<IServicioClubService, ServicioClubService>();
            services.AddScoped<ITorneoService, TorneoService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IValidator<ClubCreateRequest>, ClubCreateRequestValidator>();
            services.AddScoped<IValidator<ClubUpdateRequest>, ClubUpdateRequestValidator>();
            services.AddScoped<IValidator<ClubDistanceRequest>, ClubDistanceRequestValidator>();

            services.AddScoped<IValidator<ServicioClubCreateRequest>, ServicioClubCreateRequestValidator>();
            services.AddScoped<IValidator<ServicioClubUpdateRequest>, ServicioClubUpdateRequestValidator>();

            services.AddScoped<IValidator<TorneoCreateRequest>, TorneoCreateRequestValidator>();
            services.AddScoped<IValidator<TorneoUpdateRequest>, TorneoUpdateRequestValidator>();

            services.AddScoped<IValidator<ParticipanteTorneoCreateRequest>, ParticipanteTorneoCreateRequestValidator>();
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
