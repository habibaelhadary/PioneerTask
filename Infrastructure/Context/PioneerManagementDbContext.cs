using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domin.Models.Employees;
using Domin.Models.Employees_Propeties;
using Domin.Models.Properties;

namespace Infrastructure.Context
{
    public class PioneerManagementDbContext : DbContext
    {
        public PioneerManagementDbContext()
        {

        }
        public PioneerManagementDbContext(DbContextOptions<PioneerManagementDbContext> options) : base(options) { }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Domin.Models.Properties.Property> Properties { get; set; }
        public virtual DbSet<Employees_Properties> Employees_Properties { get; set; }
        public virtual DbSet<DropDownValues> DropDownValues { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Many to Many Realation 
            modelBuilder.Entity<Employees_Properties>()
                .HasKey(ep => new { ep.EmployeeId, ep.PropertyId }); // Composite key

            modelBuilder.Entity<Employees_Properties>()
                .HasOne(ep => ep.Employee)
                .WithMany(e => e.EmployeeProperties)
                .HasForeignKey(ep => ep.EmployeeId);

            modelBuilder.Entity<Employees_Properties>()
                .HasOne(ep => ep.Property)
                .WithMany(p => p.EmployeeProperties)
                .HasForeignKey(ep => ep.PropertyId);

            modelBuilder.Entity<Employees_Properties>()
                .Property(ep => ep.Value)
                .HasMaxLength(500);
        }
    }
}
