using GharxpertAPI.Data;
using GharxpertAPI.Models;
using GharxpertAPI.Repository.IRepository;

namespace GharxpertAPI.Repository
{
    public class QuotationStatusRepository : Repository<QuotationStatus>, IQuotationStatusRepository
    {
        private readonly ApplicationDbContext _db;
        public QuotationStatusRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<QuotationStatus> UpdateAsync(QuotationStatus entity)
        {
            _db.QuatationStatuses.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
