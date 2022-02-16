using Microsoft.AspNetCore.Identity;

namespace IssueTracker.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        // nav property
        public ICollection<Issue>? Issues { get; set; }
    }
}
