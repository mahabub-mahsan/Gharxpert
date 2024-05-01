using GharXpertWeb.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GharXpertWeb.Models.VM
{
    public class WorkTypeCreateVM
    {
        public WorkTypeCreateVM()
        {
            WorkType = new WorkTypeCreateDTO();
        }

        public WorkTypeCreateDTO WorkType { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> WorkList { get; set; }
    }
}
