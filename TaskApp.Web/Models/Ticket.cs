using System.ComponentModel.DataAnnotations;

namespace TaskApp.Web.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 30)]
        public string Description { get; set; }
        public DateOnly TaskDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        public DateTime Time { get; set; } = DateTime.Now;
        public string? User {  get; set; } 

     
    }
}
