using IssueTracker.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssueTracker.Models;

namespace IssueTracker.ViewComponents
{
    public class ProjectListViewComponent : ViewComponent
    {
        private readonly IssueTrackerContext db;

        public ProjectListViewComponent(IssueTrackerContext context)
        {
            db = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var projects = await db.Project_1.ToListAsync();
            var projects = await GetItemsAsync();
            return View(projects);
        }
        private Task<List<Project>> GetItemsAsync()
        {
            return db.Project_1.ToListAsync();
        }
    }
}
