using Microsoft.EntityFrameworkCore;
using SaveUp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveUp.Library
{
    public class AppDbContext : DbContext
    {
        public DbSet<ProductModel> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductModel>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MauiProgram).Assembly);
        }
    }
}
