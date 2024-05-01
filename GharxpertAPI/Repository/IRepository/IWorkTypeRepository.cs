using GharxpertAPI.Models;

namespace GharxpertAPI.Repository.IRepository
{
    public interface IWorkTypeRepository : IRepository<WorkType>
    {
        Task<WorkType> UpdateAsync(WorkType entity);
    }
}
