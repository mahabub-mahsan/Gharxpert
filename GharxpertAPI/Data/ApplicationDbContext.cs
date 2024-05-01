using GharxpertAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace GharxpertAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        public DbSet<Work> Works { get; set; }
        public DbSet<WorkType> WorkTypes { get; set; }
        public DbSet<ServiceCharges> ServiceCharges { get; set; }
        public DbSet<ContactOurExpert> ContactOurExperts { get; set; }
        public DbSet<LocalUser> LocalUsers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ConstructionType> ConstructionTypes { get; set; }

        public DbSet<QuotationStatus> QuatationStatuses { get; set; }
        public DbSet<QuotationDetail> QuotationDetails { get; set; }
        public DbSet<QuotationMaster> QuotationMasters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<QuotationMaster>()
                .HasMany(o => o.QuotationDetails)
                .WithOne(oi => oi.QuotationMaster)
                .HasForeignKey(oi => oi.QId);

            modelBuilder.Entity<QuotationMaster>()
            .Property(e => e.QId)
            .ValueGeneratedOnAdd();

        }
    }
}
