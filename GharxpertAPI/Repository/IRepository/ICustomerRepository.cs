using GharxpertAPI.Models;

namespace GharxpertAPI.Repository.IRepository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> UpdateAsync(Customer entity);
    }
}
