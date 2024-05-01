using GharxpertAPI.Models;

namespace GharxpertAPI.Repository.IRepository
{
    public interface IQuotationMasterRepository : IRepository<QuotationMaster>
    {
        //Task<QuotationMaster> CreateAsync(QuotationMaster entity);
        Task<QuotationMaster> UpdateAsync(QuotationMaster entity);
    }
}
