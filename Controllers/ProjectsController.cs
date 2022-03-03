#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IssueTracker.Data;
using IssueTracker.Models;

namespace IssueTracker.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IssueTrackerContext _context;

        public ProjectsController(IssueTrackerContext context)
        {
            _context = context;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            return View(await _context.Project_1.ToListAsync());
            //return RedirectToAction("Issues","Project");
        }
        public IActionResult CreateIssue(string projectName)
        {
            return RedirectToAction("Create", "Issues", new { ProjectName = projectName });
        }

        public async Task<IActionResult> Issues(string projectName, string SearchString, bool GetCurrentUserIssues, string OrderProperty, bool OrderByDescending)
        {
            var issues = from i in _context.Issue
                         where i.Discriminator == "Issue"
                         && i.ProjectName == projectName
                         select i;

            List<SelectListItem> OrderProperties = new List<SelectListItem>();
            OrderProperties.Add(new SelectListItem { Text = "Priority" });
            OrderProperties.Add(new SelectListItem { Text = "Deadline" });
            ViewData["OrderProperties"] = OrderProperties;

            // Order by various properties
            if (nameof(Issue.Priority) == OrderProperty)
            {
                if (OrderByDescending)
                    issues = issues.OrderByDescending(i => i.Priority);
                else
                    issues = issues.OrderBy(i => i.Priority);
            }

            if (nameof(Issue.Deadline) == OrderProperty)
            {
                if (OrderByDescending)
                    issues = issues.OrderByDescending(i => i.Deadline);
                else
                    issues = issues.OrderBy(i => i.Deadline);
            }

            if (!String.IsNullOrEmpty(SearchString))
            {
                issues = issues.Where(i => i.UserName.Contains(SearchString));
            }

            if (GetCurrentUserIssues)
            {
                issues = issues.Where(i => i.UserName == User.Identity.Name);
            }

            ViewData["ProjectName"] = projectName;

            return View(issues);
        }

        public async Task<IActionResult> IssueLogs(string projectName, string SearchString, bool GetCurrentUserIssues, string OrderProperty, bool OrderByDescending)
        {
            var issues = from i in _context.Issue
                         where i.Discriminator == "IssueLog"
                         && i.ProjectName == projectName
                         select i;

            List<SelectListItem> OrderProperties = new List<SelectListItem>();
            OrderProperties.Add(new SelectListItem { Text = "Priority" });
            OrderProperties.Add(new SelectListItem { Text = "Deadline" });
            ViewData["OrderProperties"] = OrderProperties;

            // Order by various properties
            if (nameof(Issue.Priority) == OrderProperty)
            {
                if (OrderByDescending)
                    issues = issues.OrderByDescending(i => i.Priority);
                else
                    issues = issues.OrderBy(i => i.Priority);
            }

            if (nameof(Issue.Deadline) == OrderProperty)
            {
                if (OrderByDescending)
                    issues = issues.OrderByDescending(i => i.Deadline);
                else
                    issues = issues.OrderBy(i => i.Deadline);
            }

            if (!String.IsNullOrEmpty(SearchString))
            {
                issues = issues.Where(i => i.UserName.Contains(SearchString));
            }

            if (GetCurrentUserIssues)
            {
                issues = issues.Where(i => i.UserName == User.Identity.Name);
            }

            ViewData["ProjectName"] = projectName;
            return View(issues);
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project_1
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project_1.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project_1
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Project_1.FindAsync(id);
            _context.Project_1.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.Project_1.Any(e => e.Id == id);
        }
    }
}
