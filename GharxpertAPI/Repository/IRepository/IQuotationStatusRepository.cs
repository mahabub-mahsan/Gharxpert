using GharxpertAPI.Models;

namespace GharxpertAPI.Repository.IRepository
{
    public interface IQuotationStatusRepository : IRepository<QuotationStatus>
    {
        Task<QuotationStatus> UpdateAsync(QuotationStatus entity);
    }
}
