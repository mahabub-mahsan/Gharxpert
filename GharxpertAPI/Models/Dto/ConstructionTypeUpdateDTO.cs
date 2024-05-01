using System.ComponentModel.DataAnnotations;

namespace GharxpertAPI.Models.Dto
{
    public class ConstructionTypeUpdateDTO
    {
        [Required]
        public int Cno { get; set; }
        [Required]
        public string Ctype { get; set; }
    }
}
