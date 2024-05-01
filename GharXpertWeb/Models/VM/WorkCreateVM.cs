using GharXpertWeb.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GharXpertWeb.Models.VM
{
    public class WorkCreateVM
    {
        public WorkCreateVM()
        {
            Work = new WorkCreateDTO();
        }

        public WorkCreateDTO Work { get; set; }
        //[ValidateNever]
        //public IEnumerable<SelectListItem> UserList { get; set; }
    }
}
