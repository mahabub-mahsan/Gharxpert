using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GharxpertAPI.Models
{
    public class ServiceCharges
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Id { get; set; }
        [ForeignKey("ConstructionType")]
        [Required]
        public int ConstructionTypeId { get; set; }
        public ConstructionType ConstructionType { get; set; }

        [ForeignKey("Work")]
        [Required]
        public int WorkId { get; set; }
        public Work Work { get; set; }

        [ForeignKey("WorkType")]
        [Required]
        public int WorkTypeId { get; set; }
        public WorkType WorkType { get; set; }
        [Required]
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
        public string Units { get; set; }

        [ForeignKey("LocalUser")]
        [Required]
        public int UserId { get; set; }
        public LocalUser User { get; set; }
    }
}
