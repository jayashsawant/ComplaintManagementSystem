using ComplaintManagementSystem.Data;
using ComplaintManagementSystem.Models;
using ComplaintManagementSystem.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ComplaintManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

       

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            ViewBag.Users = new SelectList(_context.Users, "UserId", "Name");
            return View();
        }


        [HttpPost]
        public IActionResult Create(Complaint complaint)
        {
            if (ModelState.IsValid)
            {
                complaint.Status = ComplaintStatus.Pending;
                complaint.CreatedDate = DateTime.Now;

                _context.Complaints.Add(complaint);
                _context.SaveChanges();

                return RedirectToAction("List");
            }
            ViewBag.UsersId = new SelectList(_context.Users, "UserId", "Name");
            return View(complaint);
        }

        public IActionResult List()
        {
            var complaints = _context.Complaints
                .Include(c => c.User)
                .ToList();

            return View(complaints);
        }

        public IActionResult UpdateStatus(int id)
        {
            var complaint = _context.Complaints.Find(id);
            return View(complaint);
        }

        [HttpPost]
        public IActionResult UpdateStatus(Complaint complaint)
        {
            _context.Complaints.Update(complaint);
            _context.SaveChanges();

            return RedirectToAction("List");
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
