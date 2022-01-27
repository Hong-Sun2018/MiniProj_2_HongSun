using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniProj_HongSun.Models
{
    internal class AppDbContext:DbContext
    {

        public DbSet<Mobilephone> Mobilephones { get; set; }
        public DbSet<Laptop> Laptops { get; set; }
        public DbSet<Rate> Rates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"
                Data Source=DESKTOP-RRPBPMD\SQLEXPRESS;
                Initial Catalog=MiniProject_2;
                Integrated Security=True;Connect Timeout=30;
                Encrypt=False;TrustServerCertificate=False;
                ApplicationIntent=ReadWrite;
                MultiSubnetFailover=False
            ");
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder )
        {
            modelbuilder.Entity<Mobilephone>()
                .Property(phone => phone.MobilePhoneId)
                .UseIdentityColumn(seed:1, increment:2);
            modelbuilder.Entity<Laptop>()
                .Property(laptop => laptop.LaptopId)
                .UseIdentityColumn(seed: 2, increment: 2);
        }

    }
}
