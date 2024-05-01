using GharXpert_Web.Models.Dto;
using System.ComponentModel.DataAnnotations;

namespace GharXpert_Web.Models.Dto
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
