using GharxpertAPI.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}
