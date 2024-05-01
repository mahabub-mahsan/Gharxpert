using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GharxpertAPI.Models
{
    public class WorkType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Work_Type { get; set; }
        public string WorkDesc { get; set; }
        public string WorkImage { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("Work")]
        public int WorkID { get; set; }
        public Work Work { get; set; }
    }
}
