using GharxpertAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GharxpertAPI.Models.VM
{
    public class QuotationCreateVM
    {
        public QuotationCreateVM()
        {
            QuotationMasters = new QuotationMasterCreateDTO();
        }
        public QuotationMasterCreateDTO QuotationMasters { get; set; }
        [ValidateNever]
        //public IEnumerable<SelectListItem> WorkTypeList { get; set; }

        public List<QuotationDetail> QuotationDetails { get; set; } = new List<QuotationDetail>();
    }
}
