using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Interfaces;
using Infrastructure.Logging;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ApplicationCore;

namespace CapPro.Web {
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
            
            services.AddDbContext<ManagementContext>(options => {
                try {
                     options.UseInMemoryDatabase("CapProDb");                  
                                    }
                catch (System.Exception ex) {
                    var message = ex.Message;
                }
            });
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddScoped<ICustomersService, CustomersService>();
            //  services.AddScoped<CustomersService>();
            services.Configure<ManagementSettings>(Configuration);
            services.AddMemoryCache();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        public void ConfigureDevelopment(IApplicationBuilder app,
                                IHostingEnvironment env,
                                ILoggerFactory loggerFactory,
                                ManagementContext managementContext) {
            Configure(app, env);

            //Seed Data
            ManagementContextSeed.SeedAsync(app, managementContext, loggerFactory)
            .Wait();
        }

        public void ConfigureProduction(IApplicationBuilder app,
                                        IHostingEnvironment env,
                                        ILoggerFactory loggerFactory,
                                        ManagementContext managementContext) {
            Configure(app, env);

            //Seed Data
            ManagementContextSeed.SeedAsync(app, managementContext, loggerFactory)
            .Wait();
        }
    }
}
