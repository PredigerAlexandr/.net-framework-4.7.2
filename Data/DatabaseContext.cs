using Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DatabaseContext: DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Tc> Tcs { get; set; }
        public DbSet<AssemblyInfo> AssemblyInfos { get; set; }
    }
}
