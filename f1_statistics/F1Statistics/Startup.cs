using F1Statistics.Library.DataAccess;
using F1Statistics.Library.DataAccess.Interfaces;
using F1Statistics.Library.DataAggregation;
using F1Statistics.Library.DataAggregation.Interfaces;
using F1Statistics.Library.Services;
using F1Statistics.Library.Services.Interfaces;
using F1Statistics.Library.Validators;
using F1Statistics.Library.Validators.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F1Statistics
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

            services.AddCors(options => 
            {
                options.AddDefaultPolicy(builder => 
                {
                    builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddTransient<IOptionsValidator, OptionsValidator>();

            services.AddTransient<IResultsDataAccess, ResultsDataAccess>();
            services.AddTransient<IQualifyingDataAccess, QualifyingDataAccess>();
            services.AddTransient<IStandingsDataAccess, StandingsDataAccess>();
            services.AddTransient<ILapsDataAccess, LapsDataAccess>();
            services.AddTransient<IDriversDataAccess, DriversDataAccess>();
            services.AddTransient<IConstructorsDataAccess, ConstructorsDataAccess>();
            services.AddTransient<IRacesDataAccess, RacesDataAccess>();
            services.AddTransient<IFastestDataAccess, FastestDataAccess>();

            services.AddTransient<IWinsService, WinsService>();
            services.AddTransient<IPolesService, PolesService>();
            services.AddTransient<IFastestLapsService, FastestLapsService>();
            services.AddTransient<IPointsService, PointsService>();
            services.AddTransient<IPodiumsService, PodiumsService>();
            services.AddTransient<ILeadingLapsService, LeadingLapsService>();
            services.AddTransient<IMiscService, MiscService>();
            services.AddTransient<INationalitiesService, NationalitiesService>();
            services.AddTransient<ISeasonsService, SeasonsService>();

            services.AddTransient<IWinsAggregator, WinsAggregator>();
            services.AddTransient<IPolesAggregator, PolesAggregator>();
            services.AddTransient<IFastestLapsAggregator, FastestLapsAggregator>();
            services.AddTransient<IPointsAggregator, PointsAggregator>();
            services.AddTransient<IPodiumsAggregator, PodiumsAggregator>();
            services.AddTransient<ILeadingLapsAggregator, LeadingLapsAggregator>();
            services.AddTransient<IMiscAggregator, MiscAggregator>();
            services.AddTransient<INationalitiesAggregator, NationalitiesAggregator>();
            services.AddTransient<ISeasonsAggregator, SeasonsAggregator>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "F1Statistics", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "F1Statistics v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
