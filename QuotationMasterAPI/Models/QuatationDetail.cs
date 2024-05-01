using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace QuotationMasterAPI.Models
{
    public class QuotationDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Id { get; set; }

        public int QId { get; set; }
        public int WorkTypeId { get; set; }
        public decimal lenth_in_feets { get; set; }
        public decimal width_in_feets { get; set; }
        public decimal slab_area {  get; set; }
        public int no_of_floors { get; set; }
        public decimal total_area { get; set; }
        public int QuoteId { get; set; }
        public bool StructuralWorkGroundFloor { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public QuotationMaster? QuotationMaster { get; set; }
    }
}
