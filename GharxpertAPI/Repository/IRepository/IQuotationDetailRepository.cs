using GharxpertAPI.Models;

namespace GharxpertAPI.Repository.IRepository
{
    public interface IQuotationDetailRepository : IRepository<QuotationDetail>
    {
        Task<QuotationDetail> UpdateAsync(QuotationDetail entity);
    }
}
