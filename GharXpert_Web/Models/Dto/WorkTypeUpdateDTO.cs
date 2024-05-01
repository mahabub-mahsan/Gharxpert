using System.ComponentModel.DataAnnotations;

namespace GharXpert_Web.Models.Dto
{
    public class WorkTypeUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Work_Type { get; set; }
        public string WorkDesc { get; set; }
        public string WorkImage { get; set; }
        public bool IsActive { get; set; }
        public int WorkID { get; set; }
    }
}
