using GharxpertAPI.Data;
using GharxpertAPI.Models;
using GharxpertAPI.Repository.IRepository;

namespace GharxpertAPI.Repository
{
    public class ConstructionTypeRepository : Repository<ConstructionType>, IConstructionTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public ConstructionTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ConstructionType> UpdateAsync(ConstructionType entity)
        {
            _db.ConstructionTypes.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
