using GharxpertAPI.Data;
using GharxpertAPI.Models;
using GharxpertAPI.Repository.IRepository;

namespace GharxpertAPI.Repository
{
    public class QuotationDetailRepository : Repository<QuotationDetail>, IQuotationDetailRepository
    {
        private readonly ApplicationDbContext _db;
        public QuotationDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<QuotationDetail> UpdateAsync(QuotationDetail entity)
        {
            _db.QuotationDetails.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }

}
