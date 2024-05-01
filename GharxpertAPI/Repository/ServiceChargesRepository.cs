using GharxpertAPI.Data;
using GharxpertAPI.Models;
using GharxpertAPI.Repository.IRepository;

namespace GharxpertAPI.Repository
{
    public class ServiceChargesRepository : Repository<ServiceCharges>, IServiceChargesRepository
    {
        private readonly ApplicationDbContext _db;
        public ServiceChargesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ServiceCharges> UpdateAsync(ServiceCharges entity)
        {
            _db.ServiceCharges.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
