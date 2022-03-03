using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IssueTracker.Models
{
    public class IssueLog : Issue
    {
        public bool? DeadlineMet { get; set; }

        public IssueLog() { }

        public IssueLog(Issue issue)
        {
            Title = issue.Title;
            Description = issue.Description;
            Deadline = issue.Deadline;
            DeadlineMet = issue.Deadline < DateTime.UtcNow;
            Priority = issue.Priority;
            UserName = issue.UserName;
            ProjectName = issue.ProjectName;
            Discriminator = "IssueLog";
        }
    }
}
