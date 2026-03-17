using ComplaintManagementSystem.Data;
using ComplaintManagementSystem.Models;
using ComplaintManagementSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComplaintManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 📋 Admin Dashboard - View All Complaints
        public IActionResult Dashboard()
        {
            var complaints = _context.Complaints
                .Include(c => c.User)
                .ToList();

            return View(complaints);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var complaint = _context.Complaints
                .Include(c => c.User)
                .FirstOrDefault(c => c.ComplaintId == id);

            if (complaint == null)
                return NotFound();

            var vm = new EditComplaintVM
            {
                ComplaintId = complaint.ComplaintId,
                Status = complaint.Status,
                UserName = complaint.User.Name
            };

            ViewBag.UserName = complaint.User.Name;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditComplaintVM model)
        {
            var entity = _context.Complaints
                .FirstOrDefault(c => c.ComplaintId == id);

            if (entity == null)
                return NotFound();

            entity.Status = model.Status;
            _context.SaveChanges();

            return RedirectToAction(nameof(Dashboard));
        }




        // 💾 Save Updated Status / Department
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(Complaint complaint)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Complaints.Update(complaint);
        //        _context.SaveChanges();
        //        return RedirectToAction(nameof(Dashboard));
        //    }

        //    // Reload User so the view doesn't crash
        //    complaint.User = _context.Users
        //        .FirstOrDefault(u => u.UserId == complaint.UserId);

        //    return View(complaint);
        //}

    }
}
