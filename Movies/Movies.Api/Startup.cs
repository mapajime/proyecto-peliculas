using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movies.Api.Configuration;
using Movies.Business.Implementations;
using Movies.Business.Interfaces;
using Movies.DataAccess.Context;
using Movies.DataAccess.Repositories.Implementation;
using Movies.DataAccess.Repositories.Interfaces;
using System.Linq;

namespace Movies.Api
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
            var mapperConfig = new MapperConfiguration(m => m.AddProfile(new MappingProfile()));
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddDbContext<MovieContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("movies")));
            services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
            });
            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IGenderRepository, GenderRepository>();
            services.AddScoped<IMovieGenderRepository, MovieGenderRepository>();
            services.AddScoped<IActorBusiness, ActorBusiness>();
            services.AddScoped<ICountryBusiness, CountryBusiness>();
            services.AddScoped<ILanguageBusiness, LanguageBusiness>();
            services.AddScoped<IMovieGenderBusiness, MovieGenderBusiness>();
            services.AddScoped<IMovieBusiness, MovieBusiness>();
            services.AddScoped<IGenderBusiness, GenderBusiness>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movies API");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}