using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IssueTracker.Models
{
    public class Project
    {
        public int Id { get; set; }

        [DisplayName("Name")]
        [Required]
        public string Name { get; set; }
        public ICollection<Issue>? Issues { get; set; }
    }
}
