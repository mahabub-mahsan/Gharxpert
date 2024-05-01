using GharxpertAPI.Data;
using GharxpertAPI.Models;
using GharxpertAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace GharxpertAPI.Repository
{
    public class QuotationMasterRepository : Repository<QuotationMaster>, IQuotationMasterRepository
    {
        private readonly ApplicationDbContext _db;
        public QuotationMasterRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        //public async Task<QuotationMaster> CreateAsync(QuotationMaster entity)
        //{
        //    _db.QuotationMasters.Add(entity);
        //    await _db.SaveChangesAsync();
        //    return entity;
        //}

        public async Task<QuotationMaster> UpdateAsync(QuotationMaster entity)
        {
            _db.QuotationMasters.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }

}
