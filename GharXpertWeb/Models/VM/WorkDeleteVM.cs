using GharXpertWeb.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GharXpertWeb.Models.VM
{
    public class WorkDeleteVM
    {
        public WorkDeleteVM()
        {
            Work = new WorkDTO();
        }

        public WorkDTO Work { get; set; }
        //[ValidateNever]
        //public IEnumerable<SelectListItem> UserList { get; set; }
    }
}
