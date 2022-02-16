namespace IssueTracker.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime Deadline { get; set; }
        public int Priority { get; set; }

        //public virtual CompletedBy? CompletedBy { get; set; }
        //public virtual AssignedTo? AssignedTo { get; set; }
        
        // nav properties
        public string? UserName { get; set; }
        //public User User { get; set; }
    }
}
