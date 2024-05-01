using System.ComponentModel.DataAnnotations;

namespace GharxpertAPI.Models.Dto
{
    public class WorkCreateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string WorkName { get; set; }
        public string WorkDescription { get; set; }
        public string WorkImage { get; set; }
        public Boolean WorkIsActive { get; set; }
        public int UserId { get; set; }
    }
}
