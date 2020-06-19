using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp1.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<CleaningPlace> CleaningPlaces { get; set; }
        public DbSet<Cleaning> Cleanings { get; set; }

        public ApplicationContext() {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=DataBase.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(e => e._WorkTimes).HasColumnName("WorkTimes");

            modelBuilder.Entity<CleaningPlace>()
                .Property(cp => cp._WorkTime).HasColumnName("WorkTime");

            modelBuilder.Entity<Client>()
                .Property(c => c._StartTime).HasColumnName("StartTime");
        }
    }
}
