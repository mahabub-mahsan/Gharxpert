using System.ComponentModel.DataAnnotations;

namespace GharXpertWeb.Models.Dto
{
    public class ServiceChargesDTO
    {

        [Required]
        public int Id { get; set; }
        [Required]
        public int ConstructionTypeId { get; set; }
        [Required]
        public int WorkId { get; set; }
        [Required]
        public int WorkTypeId { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
        public string Units { get; set; }
        public int UserId { get; set; }

        public ConstructionTypeDTO ConstructionType { get; set; }
        public WorkDTO Work { get; set; }
        public WorkTypeDTO WorkType { get; set; }
        public UserDTO User { get; set; }
    }
}
