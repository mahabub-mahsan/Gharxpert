using System.ComponentModel.DataAnnotations;

namespace GharxpertAPI.Models.Dto
{
    public class QuotationStatusDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
