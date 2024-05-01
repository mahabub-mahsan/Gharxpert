using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuotationMasterAPI.Models
{
    public class QuotationMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int QId { get; set; }
        public int WorkId { get; set; }
        public DateTime QDate { get; set; }
        public int CustomerId { get; set; }
        public int WorkTypeId { get; set; }
        public int UserId { get; set; }
        public int QSId { get; set; }
        public List<QuotationDetail> QuotationDetails { get; set; } = new List<QuotationDetail>();

    }
}
