using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Persistance;
using Service;

namespace api
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
            var connection = Configuration.GetConnectionString("DefaultConnection");
            //string connection = "Host=localhost;Port=5432;Database=GMZ;Username=postgres;Password=805652bb28054";
            services.AddDbContext<GMZDbContext>(options => options.UseNpgsql(connection, b =>b.MigrationsAssembly("Persistance")));
            //services.AddDbContext<GMZDbContext>(options => options.UseNpgsql(connection));
            services.AddTransient<IEmployService, EmployService>(provider => new EmployService(connection));
            //services.AddTransient<IEmployService, EmployService>();
            services.AddCors(options => 
                    options.AddPolicy("AllowSpecificOrigin", builder => 
                            builder.AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowAnyOrigin())
                            );
              services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowSpecificOrigin");

            app.UseMvc();
        }
    }
}
