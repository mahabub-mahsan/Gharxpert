using System.ComponentModel.DataAnnotations;
using GharxpertAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;

namespace GharxpertAPI.Models.Dto
{
    public class WorkDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string WorkName { get; set; }
        public string WorkDescription { get; set; }
        public string WorkImage { get; set; }
        public Boolean WorkIsActive { get; set; }
    }
}
