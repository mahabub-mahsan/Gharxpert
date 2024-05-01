using GharxpertAPI.Models;

namespace GharxpertAPI.Repository.IRepository
{
    public interface IServiceChargesRepository : IRepository<ServiceCharges>
    {
        Task<ServiceCharges> UpdateAsync(ServiceCharges entity);
    }
}
