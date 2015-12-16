using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EmptyDB.Domain
{
    public class User : IdentityUser<Guid>
    {
        public DateTimeOffset CreatedOn { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
