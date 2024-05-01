using System.ComponentModel.DataAnnotations;

namespace GharxpertAPI.Models.Dto
{
    public class QuotationMasterCreateDTO
    {
        [Required]
        public int QId { get; set; }
        [Required]
        public int WorkId { get; set; }
        public DateTime QDate { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int WorkTypeId { get; set; }
        [Required]
        public int UserId { get; set; }
        public int QSId { get; set; }

        public List<QuotationDetailCreateDTO> QuotationDetails { get; set; } = new List<QuotationDetailCreateDTO>();
    }
}
