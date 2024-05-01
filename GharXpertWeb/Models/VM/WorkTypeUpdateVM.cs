using GharXpertWeb.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GharXpertWeb.Models.VM
{
    public class WorkTypeUpdateVM
    {
        public WorkTypeUpdateVM() 
        {
            WorkType = new WorkTypeUpdateDTO();
        }

        public WorkTypeUpdateDTO WorkType { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> WorkList { get; set; }
    }
}
