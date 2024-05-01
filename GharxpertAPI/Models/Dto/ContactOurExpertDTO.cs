using System.ComponentModel.DataAnnotations;

namespace GharxpertAPI.Models.Dto
{
    public class ContactOurExpertDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string CName { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(20)]
        public string Mobile { get; set; }
        [MaxLength(20)]
        public string Landline { get; set; }

        public bool ConstructionWork { get; set; }
        public bool ElectricalWork { get; set; }
        public bool PlumbingWork { get; set; }
        public bool Doors { get; set; }
        public bool Windows { get; set; }
        public bool Tiles { get; set; }
        public bool Granite { get; set; }
        public bool FalseCeiling { get; set; }
        public bool Paint { get; set; }
        public bool MS_And_SS_Work { get; set; }
        public string Status { get; set; }
        public string Employee {  get; set; }
        public string CustomerResponse { get; set; }
        public DateTime Meeting { get; set; }
    }
}
