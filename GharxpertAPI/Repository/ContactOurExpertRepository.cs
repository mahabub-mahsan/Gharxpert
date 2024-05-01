using GharxpertAPI.Data;
using GharxpertAPI.Models;
using GharxpertAPI.Repository.IRepository;

namespace GharxpertAPI.Repository
{
    public class ContactOurExpertRepository : Repository<ContactOurExpert>, IContactOurExpertRepository
    {
        private readonly ApplicationDbContext _db;
        public ContactOurExpertRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ContactOurExpert> UpdateAsync(ContactOurExpert entity)
        {
            _db.ContactOurExperts.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
