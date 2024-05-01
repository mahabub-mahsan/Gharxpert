using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GharxpertAPI.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int CustomerId { get; set; }
        [Required]
        [StringLength(50)]
        public string CName { get; set; }
        [Required]
        public string Mobile {  get; set; }
        [Required]
        public string Address { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Landline { get; set; }
    }
}
