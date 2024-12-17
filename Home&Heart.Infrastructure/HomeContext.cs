using Home_Heart.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home_Heart.Infrastructure
{
    public class HomeContext : DbContext
    {
        public HomeContext(DbContextOptions options) : base(options) { }
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
    }
}
