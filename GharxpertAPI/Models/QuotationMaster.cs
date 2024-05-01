using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GharxpertAPI.Models
{
    public class QuotationMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int QId { get; set; }
        [ForeignKey("Work")]
        [Required]
        public int WorkId { get; set; }
        public Work Work {  get; set; }
        public DateTime QDate { get; set; }
        [ForeignKey("Customer")]
        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        [ForeignKey("WorkType")]
        [Required]
        public int WorkTypeId { get; set; }
        public WorkType WorkType { get; set; }
        [ForeignKey("User")]
        [Required]
        public int UserId { get; set; }
        public LocalUser User { get; set; }
        [ForeignKey("QuatationStatus")]
        public int QSId { get; set; }
        public QuotationStatus QuatationStatus { get; set; }

        public List<QuotationDetail> QuotationDetails { get; set; } = new List<QuotationDetail>();

    }
}
