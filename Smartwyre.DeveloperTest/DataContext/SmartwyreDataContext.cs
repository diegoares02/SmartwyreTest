using System;
using Microsoft.EntityFrameworkCore;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.DataContext
{
    public class SmartwyreDataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Rebate> Rebates { get; set; }
        public DbSet<RebateCalculation> RebateCalculations { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("SmartwyreDatabase");
        }
    }
}
