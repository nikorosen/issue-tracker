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

        public DbSet<IssueTracker.Models.Issue> Issue { get; set; }
        public DbSet<User> User { get; set; }
    }
}
