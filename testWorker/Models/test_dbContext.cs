using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;

namespace testWorker.Models
{
    public partial class test_dbContext : DbContext
    {
        public test_dbContext()
        {
        }

        public test_dbContext(DbContextOptions<test_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<CustomerSite> CustomerSites { get; set; } = null!;

        public async Task<DateTime> TestFunc(){
            //return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<DateTime>("sch.test_func");
            //this.Data
            var x=await this.Customers.ToListAsync();
            return DateTime.Now;
            //this.Database.ExecuteSqlRaw()
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=test_db;Username=postgres;Password=postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


//builder.HasDbFunction(typeof(ChemistryContext).GetMethod(nameof(Sub), new[] { typeof(DateTime) })).HasName("sch.test_func");

 
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("customer_code_PK");

                entity.ToTable("customer", "sch");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .HasColumnName("code");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasColumnName("address");

                entity.Property(e => e.LastYearExpense).HasColumnName("last_year_expense");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<CustomerSite>(entity =>
            {
                entity.HasKey(e => new { e.Customer, e.CustomerSiteCode })
                    .HasName("customer_site_PK");

                entity.ToTable("customer_site", "sch");

                entity.Property(e => e.Customer)
                    .HasMaxLength(10)
                    .HasColumnName("customer");

                entity.Property(e => e.CustomerSiteCode)
                    .HasMaxLength(10)
                    .HasColumnName("customer_site_code");

                entity.Property(e => e.AdditionalAddress)
                    .HasMaxLength(50)
                    .HasColumnName("additional_address");

                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p.CustomerSites)
                    .HasForeignKey(d => d.Customer)
                    .HasConstraintName("customer_site_customer_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
