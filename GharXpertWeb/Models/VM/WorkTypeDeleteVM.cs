using GharXpertWeb.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GharXpertWeb.Models.VM
{
    public class WorkTypeDeleteVM
    {
        public WorkTypeDeleteVM()
        {
            WorkType = new WorkTypeDTO();
        }
        public WorkTypeDTO WorkType { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> WorkList { get; set; }

    }
}
