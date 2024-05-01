using GharxpertAPI.Models;

namespace GharxpertAPI.Repository.IRepository
{
    public interface IContactOurExpertRepository : IRepository<ContactOurExpert>
    {
        Task<ContactOurExpert> UpdateAsync(ContactOurExpert entity);
    }
}
