using System.ComponentModel.DataAnnotations;

namespace GharxpertAPI.Models.Dto
{
    public class QuotationStatusCreateDTO
    {
        [Required]
        public string Status { get; set; }
    }
}
