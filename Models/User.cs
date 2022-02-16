using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IssueTracker.Models
{
    public class User : IdentityUser
    {
        [DisplayName("First name")]
        [StringLength(15, MinimumLength = 3)]
        [Required]
        public string? FirstName { get; set; }

        [DisplayName("Last name")]
        [StringLength(15, MinimumLength = 3)]
        [Required]
        public string? LastName { get; set; }

        // nav property
        public ICollection<Issue>? Issues { get; set; }
    }
}
