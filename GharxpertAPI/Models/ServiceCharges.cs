using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GharxpertAPI.Models
{
    public class ServiceCharges
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Id { get; set; }
        [ForeignKey("WorkType")]
        [Required]
        public int WorkTypeId { get; set; }
        public WorkType WorkType { get; set; }
        [Required]
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
    }
}
