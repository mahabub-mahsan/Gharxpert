using GharXpertWeb.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GharXpertWeb.Models.VM
{
    public class ServiceChargesDeleteVM
    {
        public ServiceChargesDeleteVM()
        {
            ServiceCharges = new ServiceChargesDTO();
        }
        public ServiceChargesDTO ServiceCharges { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> WorkTypeList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> WorkList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ConstructionTypeList { get; set; }
    }
}
