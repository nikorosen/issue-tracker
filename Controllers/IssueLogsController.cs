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
    public class IssueLogsController : Controller
    {
        private readonly IssueTrackerContext _context;

        public IssueLogsController(IssueTrackerContext context)
        {
            _context = context;
        }

        // GET: IssueLogs
        public async Task<IActionResult> Index(string SearchString, bool GetCurrentUserIssues, string OrderProperty, bool OrderByDescending, bool GetCompletedIssues)
        {
            var issues = from i in _context.IssueLog select i; 

            List < SelectListItem > OrderProperties = new List<SelectListItem>();
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

            return View(issues);
        }

        // GET: IssueLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueLog = await _context.IssueLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issueLog == null)
            {
                return NotFound();
            }

            return View(issueLog);
        }

        // GET: IssueLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IssueLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeadlineMet,Id,Discriminator,Title,Description,Deadline,Priority,UserName")] IssueLog issueLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(issueLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(issueLog);
        }

        // GET: IssueLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueLog = await _context.IssueLog.FindAsync(id);
            if (issueLog == null)
            {
                return NotFound();
            }
            return View(issueLog);
        }

        // POST: IssueLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeadlineMet,Id,Discriminator,Title,Description,Deadline,Priority,UserName")] IssueLog issueLog)
        {
            if (id != issueLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issueLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueLogExists(issueLog.Id))
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
            return View(issueLog);
        }

        // GET: IssueLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issueLog = await _context.IssueLog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issueLog == null)
            {
                return NotFound();
            }

            return View(issueLog);
        }

        // POST: IssueLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issueLog = await _context.IssueLog.FindAsync(id);
            _context.IssueLog.Remove(issueLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueLogExists(int id)
        {
            return _context.IssueLog.Any(e => e.Id == id);
        }
    }
}
