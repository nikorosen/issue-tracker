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
    public class IssuesController : Controller
    {
        private readonly IssueTrackerContext _context;
        
        public IssuesController(IssueTrackerContext context)
        {
            _context = context;
        }

        // GET: Issues
        public async Task<IActionResult> Index(string SearchString, bool GetCurrentUserIssues, bool OrderByPriority, string OrderProperty)
        {
            var issues = from i in _context.Issue select i;

            //if (OrderByPriority)
            //{
            //    issues = issues.OrderByDescending(i => i.Priority);
            //}

            List<SelectListItem> OrderProperties = new List<SelectListItem>();
            OrderProperties.Add(new SelectListItem { Text = "Priority" });
            OrderProperties.Add(new SelectListItem { Text = "Deadline" });
            ViewData["OrderProperties"] = OrderProperties;


            // Order by various properties
            if (nameof(Issue.Priority) == OrderProperty)
            {
                issues = issues.OrderByDescending(i => i.Priority);
            }

            if (nameof(Issue.Deadline) == OrderProperty)
            {
                issues = issues.OrderByDescending(i => i.Deadline);
            }


            if (!String.IsNullOrEmpty(SearchString))
            {
                issues = issues.Where(i => i.UserName.Contains(SearchString));
            }

            if (GetCurrentUserIssues)
            {
                issues = issues.Where(i => i.UserName.Contains(User.Identity.Name));
            }
            

            return View(issues); //await _context.Issue.ToListAsync());
        }

        // GET: Issues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issue
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // GET: Issues/Create
        public async Task<IActionResult> Create()
        {
            List<User> users = new List<User>();
                IQueryable<string> userQuery = from m in _context.User
                                               select m.UserName;

            SelectList userNames = new SelectList(await userQuery.ToListAsync());
            ViewData["UserName"] = userNames;

            return View();
        }

        // POST: Issues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Deadline,Priority,UserName")] Issue issue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(issue);

                if (issue.UserName != null)
                {
                    var user = await _context.User.FirstOrDefaultAsync(m => m.UserName == issue.UserName);
                    
                    if (user.Issues == null)
                        user.Issues = new List<Issue>();
                    
                    user.Issues.Add(issue);
                    _context.Update(user);
                }
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(issue);
        }

        // GET: Issues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issue.FindAsync(id);
            if (issue == null)
            {
                return NotFound();
            }

            // Get users
            List<User> users = new List<User>();
            IQueryable<string> userQuery = from m in _context.User
                                           select m.UserName;

            List<SelectListItem> userNames = new SelectList(await userQuery.ToListAsync()).ToList();

            //
            // TODO: remove duplicate usernames from selectlist on view side
            //
            if (issue.UserName != null)
            { 
                userNames.Insert(0, new SelectListItem { Text = issue.UserName });
            }
            else
            {
                userNames.Insert(0, new SelectListItem { Text = "Please Select a User" });
            }

            ViewData["UserName"] = userNames;

            return View(issue);
        }

        // POST: Issues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Deadline,Priority,UserName")] Issue issue)
        {
            if (id != issue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issue);

                    // also update user
                    if (issue.UserName != null)
                    {
                        var user = await _context.User.FirstOrDefaultAsync(m => m.UserName == issue.UserName);

                        if (user.Issues == null)
                            user.Issues = new List<Issue>();

                        user.Issues.Add(issue);
                        _context.Update(user);
                    }


                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueExists(issue.Id))
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
            return View(issue);
        }

        // GET: Issues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issue
                .FirstOrDefaultAsync(m => m.Id == id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // POST: Issues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issue = await _context.Issue.FindAsync(id);
            _context.Issue.Remove(issue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueExists(int id)
        {
            return _context.Issue.Any(e => e.Id == id);
        }
    }
}
