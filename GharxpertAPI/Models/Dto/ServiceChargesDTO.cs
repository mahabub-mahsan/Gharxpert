using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace GharxpertAPI.Models.Dto
{
    public class ServiceChargesDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int WorkTypeId { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
        public WorkTypeDTO WorkType { get; set; }
    }
}
