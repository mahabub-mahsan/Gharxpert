using GharXpertWeb.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GharXpertWeb.Models.VM
{
    public class WorkUpdateVM
    {
        public WorkUpdateVM()
        {
            Work = new WorkUpdateDTO();
        }

        public WorkUpdateDTO Work { get; set; }
        //[ValidateNever]
        //public IEnumerable<SelectListItem> UserList { get; set; }
    }
}
