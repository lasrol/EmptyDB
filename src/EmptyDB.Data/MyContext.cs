using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using EmptyDB.Domain;

namespace EmptyDB.Data
{
    public class MyContext : DbContext
    {
        public DbSet<Foo> Foos { get; set; }
        public DbSet<Bar> Bars { get; set; }
    }
}
