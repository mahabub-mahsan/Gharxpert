using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GharxpertAPI.Models
{
    public class Work
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public string WorkName { get; set; }
        public string WorkDescription { get; set; }
        public string WorkImage { get; set; }
        public Boolean WorkIsActive { get; set; }
    }
}
