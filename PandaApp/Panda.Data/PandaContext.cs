using Microsoft.EntityFrameworkCore;
using Panda.Models;
using System;

namespace Panda.Data
{
    public class PandaDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Receipt> Receipts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString.connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }

}
