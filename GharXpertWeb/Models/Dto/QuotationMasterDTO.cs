using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GharXpertWeb.Models.Dto
{
    public class QuotationMasterDTO
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

        public WorkDTO Work { get; set; }
        public CustomerDTO Customer { get; set; }
        public WorkTypeDTO WorkType { get; set; }
        public UserDTO User { get; set; }

    }
}
