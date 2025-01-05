using System.ComponentModel.DataAnnotations;

namespace PlanningAppMvc.Models
{
    public class Plan
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string? Title { get; set; }
        public string? Description { get; set; }
        [Required]
        public DateTime DoneDate { get; set; }
        [Required]
        public DateTime CreatDate { get; set; }
    }
}
