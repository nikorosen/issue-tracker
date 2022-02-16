using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IssueTracker.Models
{
    public class Issue
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string? Title { get; set; }
        [StringLength(250, MinimumLength = 3)]
        [Required]
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime Deadline { get; set; }
        [Range(1, 10)]
        [Required]
        public int Priority { get; set; }

        //public virtual CompletedBy? CompletedBy { get; set; }
        //public virtual AssignedTo? AssignedTo { get; set; }

        // nav properties
        [DisplayName("Assigned user")]
        public string? UserName { get; set; }
        //public User User { get; set; }
    }
}
