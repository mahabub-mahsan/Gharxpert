using System.ComponentModel.DataAnnotations;

namespace GharxpertAPI.Models.Dto
{
    public class ConstructionTypeCreateDTO
    {
        [Required]
        public int Cno { get; set; }
        [Required]
        public string Ctype { get; set; }
    }
}
