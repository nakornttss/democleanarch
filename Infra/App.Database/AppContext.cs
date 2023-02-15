using System;
using App.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace App.Database
{
    public class AppContext : DbContext
    {
        public DbSet<ResultLog> ResultLogs { get; set; }
        public AppContext()
        {
        }

        public AppContext(DbContextOptions<AppContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Integrated Security=true;Initial Catalog=cleandb;TrustServerCertificate=True");
            }
        }
    }
}
