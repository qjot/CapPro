using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class ManagementContext : DbContext
    {
        public ManagementContext(DbContextOptions<ManagementContext> options) : base(options) {}

        public DbSet<Customer> Customers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<Customer>(ConfigureCustomer);
        }

        private void ConfigureCustomer(EntityTypeBuilder<Customer> builder) {
            builder.ToTable("Customer");

            builder.Property(ci => ci.ID)
                .ForSqlServerUseSequenceHiLo("customer_hilo")
                .IsRequired();

            builder.Property(ci => ci.name)
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(ci => ci.surname)
                .IsRequired(true)
                .HasMaxLength(100);

            builder.Property(ci => ci.telephoneNumber)
                .IsRequired(true)
                .HasMaxLength(15);

            builder.Property(ci => ci.address)
                .IsRequired(true)
                .HasMaxLength(100);
        }
    }

   
}
