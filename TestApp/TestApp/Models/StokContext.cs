using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Models
{
    public partial class StokContext : DbContext
    {
        public StokContext()
        {

        }
        public StokContext(DbContextOptions<StokContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=UMUT;Database=TEST;Trusted_Connection=True;MultipleActiveResultSets=True;");
            }

        }
        public DbSet<STKObject> Test { get; set; } //Temp Tablomuz
        public DbSet<STKTable> STK { get; set; } //STK Tablosu

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

       
    }
}
