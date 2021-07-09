using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using ITechArt.Repositories;
using ITechArt.Surveys.Repositories;
using Microsoft.EntityFrameworkCore;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace ITechArt.Surveys.WebApp
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
            var connectionString = Configuration.GetValue<string>("connectionString");
            services.AddDbContext<SurveysDbContext>(x => x.UseSqlServer(connectionString));

            services.AddScoped<SurveyRepository>(x =>
                new SurveyRepository(x.GetService<SurveysDbContext>()));

            services.AddScoped<SectionRepository>(x =>
                new SectionRepository(x.GetService<SurveysDbContext>()));

            services.AddScoped<CounterRepository>(x =>
                new CounterRepository(x.GetService<SurveysDbContext>()));

            services.AddScoped<UnitOfWork>(x =>
                new UnitOfWork(x.GetService<SurveysDbContext>(),
                               x.GetService<SurveyRepository>(),
                               x.GetService<SectionRepository>(),
                               x.GetService<CounterRepository>()));

            AddMapper(services);

            services.AddControllersWithViews();
        }

        private void AddMapper(IServiceCollection services)
        {
            var configExp = new MapperConfigurationExpression();
            // there will be configuration like configExp.CreateMap<>().ForMember() ...

            var mapperConfig = new MapperConfiguration(configExp);
            services.AddScoped<Mapper>(x => new Mapper(mapperConfig));
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
