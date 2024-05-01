using QuotationMasterAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace QuotationMasterAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        public DbSet<QuotationDetail> QuotationDetails { get; set; }
        public DbSet<QuotationMaster> QuotationMasters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<QuotationMaster>()
                .HasMany(o => o.QuotationDetails)
                .WithOne(oi => oi.QuotationMaster)
                .HasForeignKey(oi => oi.QId);

        }
    }
}
