using API.Errors;
using API.Extensions;
using API.Helpers;
using API.Middleware;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace API
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
          
            services.AddAutoMapper(typeof(AutoMapperProfiles));
            services.AddDbContext<StoreContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
            services.AddApplicationServices();
            services.AddSwaggerDocumentation();
            services.AddCors(options =>{
                options.AddPolicy("CorsPolicy",policy =>
                {
                     policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                })
            })
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // <--Middleware-->
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {         
            app.UseMiddleware<ExceptionMiddleware>();
            // if (env.IsDevelopment())
            // {
            // }

            app.UseSwaggerDocumentation();
          
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
