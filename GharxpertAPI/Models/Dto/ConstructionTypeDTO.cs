using System.ComponentModel.DataAnnotations;

namespace GharxpertAPI.Models.Dto
{
    public class ConstructionTypeDTO
    {
        [Required]
        public int Cno { get; set; }
        [Required]
        public string Ctype { get; set; }
    }
}
