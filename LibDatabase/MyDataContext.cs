using LibDatabase.Entities;
using LibDatabase.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibDatabase
{
    public class MyDataContext : DbContext
    {
        public MyDataContext()
        {
            this.Database.Migrate();
        }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Basket> Baskets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbConnection = MyAppConfig.GetSectionValue("ConnectionDB");

            optionsBuilder.UseNpgsql(dbConnection);
            //optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=wpfdata;User Id=postgres;Password=123456;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Basket>(basket =>
            {
                basket.HasKey(b => new { b.UserId, b.ProductId });
            });
        }
    }
}
