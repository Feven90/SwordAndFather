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
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SwordAndFather
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1); //  services.AddMvc() and  app.UseMvc() is the one that makes api work
            services.Configure<DbConfiguration>(Configuration); // we are telling ASP.Net how to build things on this line and the above line
            services.AddTransient<TargetRepository>(); //AddTarsient will give new Targetrepository every time anyone asks 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts(); // my applicaton only responds to http requests only
            }

            app.UseHttpsRedirection(); // if someone makes http request send them to https 
            app.UseMvc();
        }
    }

    public class DbConfiguration
    {
        public string ConnectionString { get; set; }
    }
}
