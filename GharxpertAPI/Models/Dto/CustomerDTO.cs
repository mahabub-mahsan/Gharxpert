using System.ComponentModel.DataAnnotations;

namespace GharxpertAPI.Models.Dto
{
    public class CustomerDTO
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        [StringLength(50)]
        public string CName { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public string Address { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Landline { get; set; }
    }
}
