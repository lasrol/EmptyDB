using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmptyDB.Data;
using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Entity;

namespace Nettgrav.Data
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //TODO: Get from config
            services.AddEntityFramework().AddSqlServer().AddDbContext<MyContext>(o => o.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=EmptyDB;Trusted_Connection=True;").MigrationsAssembly("EmptyDB.Migrations"));
        }

        public void Configure(IApplicationBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        }
    }
}
