using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using EmptyDB.Domain;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EmptyDB.Data
{
    public class MyContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Foo> Foos { get; set; }
        public DbSet<Bar> Bars { get; set; }
    }
}
