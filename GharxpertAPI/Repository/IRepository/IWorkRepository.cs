using GharxpertAPI.Models;

namespace GharxpertAPI.Repository.IRepository
{
    public interface IWorkRepository : IRepository<Work>
    {
        Task<Work> UpdateAsync(Work entity);
    }
}
