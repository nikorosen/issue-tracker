#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IssueTracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IssueTracker.Data
{
    public class IssueTrackerContext : IdentityDbContext
    {
        public IssueTrackerContext (DbContextOptions<IssueTrackerContext> options)
            : base(options)
        {
        }

        public DbSet<Issue> Project { get; set; }
        public DbSet<Issue> Issue { get; set; }
        public DbSet<IssueLog> IssueLog { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<IssueTracker.Models.Project> Project_1 { get; set; }
    }
}
