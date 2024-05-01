using Microsoft.Build.Framework;

namespace GharxpertAPI.Models.Dto
{
    public class ServiceChargesUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int WorkTypeId { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
    }
}
