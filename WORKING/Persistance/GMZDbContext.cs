using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistance
{
    public class GMZDbContext : DbContext
    {
        public DbSet<Empoy> employees { get; set; }
        public GMZDbContext(DbContextOptions<GMZDbContext> options)
           : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
