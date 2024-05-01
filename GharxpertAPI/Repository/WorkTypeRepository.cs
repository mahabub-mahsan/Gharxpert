using GharxpertAPI.Data;
using GharxpertAPI.Models;
using GharxpertAPI.Repository.IRepository;

namespace GharxpertAPI.Repository
{
    public class WorkTypeRepository : Repository<WorkType>, IWorkTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public WorkTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<WorkType> UpdateAsync(WorkType entity)
        {
            _db.WorkTypes.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
