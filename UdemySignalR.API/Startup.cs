using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UdemySignalR.API.Hubs;
using UdemySignalR.API.Models;

namespace UdemySignalR.API
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsAll", builder => builder.WithOrigins("https://localhost:44348").AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            });
            services.AddControllers();
            //Kütüphaneyi kullandığımızı bildiriyoruz.
            services.AddSignalR();

            services.AddDbContext<AppDbContext>(builder => builder.UseSqlServer(Configuration["ConStr"].ToString()));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsAll");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //http://localhost:4400/Myhub
                //Hubımıza ulaşmak için endpointini oluşturuyoruz
                endpoints.MapHub<MyHub>("/MyHub");
            });
        }
    }
}
