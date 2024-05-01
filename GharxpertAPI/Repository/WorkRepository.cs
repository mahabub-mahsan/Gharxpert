using GharxpertAPI.Data;
using GharxpertAPI.Models;
using GharxpertAPI.Repository.IRepository;

namespace GharxpertAPI.Repository
{
    public class WorkRepository : Repository<Work>, IWorkRepository
    {
        private readonly ApplicationDbContext _db;

        public WorkRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
        public async Task<Work> UpdateAsync(Work entity)
        {
            _db.Works.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
