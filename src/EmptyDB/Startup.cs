using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmptyDB.Config;
using EmptyDB.Data;
using EmptyDB.Domain;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;

namespace EmptyDB
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDataProtection();
            //services.AddEntityFramework().AddDbContext<MyContext>(o => o.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=EmptyDB;Trusted_Connection=True;"));
            services.AddEntityFramework().AddInMemoryDatabase().AddDbContext<MyContext>(o => o.UseInMemoryDatabase());

            services.AddIdentity<User, Role>(IdConfig.Get)
                .AddEntityFrameworkStores<MyContext, Guid>()
                .AddDefaultTokenProviders();
            services.AddAuthentication();
            services.AddMvc();

            var provider = services.BuildServiceProvider();

            Migration.SeedAsync(provider.GetService<UserManager<User>>()).Wait();

            return provider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {    
            app.UseIISPlatformHandler();
            app.UseDeveloperExceptionPage();
            app.UseIdentity();
            app.UseMvcWithDefaultRoute();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
