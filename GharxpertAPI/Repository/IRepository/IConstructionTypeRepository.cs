using GharxpertAPI.Models;

namespace GharxpertAPI.Repository.IRepository
{
    public interface IConstructionTypeRepository : IRepository<ConstructionType>
    {
        Task<ConstructionType> UpdateAsync(ConstructionType entity);
    }
}
