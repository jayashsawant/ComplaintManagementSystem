using ComplaintManagementSystem.Data;
using ComplaintManagementSystem.Models;
using ComplaintManagementSystem.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ComplaintManagementSystem.Controllers
{
    public class ComplaintController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ComplaintController> _logger;

        public ComplaintController(ApplicationDbContext context, ILogger<ComplaintController> logger)
        {
            _context = context;
            _logger = logger;
        }


        public IActionResult Create()
        {
            ViewBag.UserId = new SelectList(_context.Users, "UserId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Complaint complaint)
        {
            _logger.LogTrace("Entered Create method");
            if (ModelState.IsValid)
            {
                _logger.LogDebug("Complaint object received: {@Complaint}", complaint);

                try
                {
                    complaint.Status = ComplaintStatus.Pending;
                    complaint.CreatedDate = DateTime.Now;

                    _context.Complaints.Add(complaint);
                    _context.SaveChanges();

                    _logger.LogInformation("Complaint created successfully for UserId: {UserId}", complaint.UserId);
                    return RedirectToAction(nameof(List));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while saving complaint");
                }
            }
            else
            {
                _logger.LogWarning("Invalid model state for complaint");
            }
            ViewBag.UserId = new SelectList(_context.Users, "UserId", "Name", complaint.UserId);
            return View(complaint);
        }

        
        public IActionResult List()
        {
            _logger.LogInformation("Fetching complaint list");
            var complaints = _context.Complaints
                .Include(c => c.User)
                .ToList();

            return View(complaints);
        }

      
        public IActionResult Details(int id)
        {
            var complaint = _context.Complaints
                .Include(c => c.User)
                .FirstOrDefault(c => c.ComplaintId == id);

            if (complaint == null)
                return NotFound();

            return View(complaint);
        }

      
        public IActionResult Delete(int id)
        {
            var complaint = _context.Complaints
                .Include(c => c.User)
                .FirstOrDefault(c => c.ComplaintId == id);

            if (complaint == null)
                return NotFound();

            return View(complaint);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var complaint = _context.Complaints.Find(id);

            if (complaint != null)
            {
                _context.Complaints.Remove(complaint);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(List));
        }

       
        public IActionResult UpdateStatus(int id)
        {

            var complaint = _context.Complaints
                .Include(c => c.User)
                .FirstOrDefault(c => c.ComplaintId == id);

            if (complaint == null)
                return NotFound();

            return View(complaint);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateStatus(Complaint complaint)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Updating status for ComplaintId: {Id}", complaint.ComplaintId);
                _context.Complaints.Update(complaint);
                _context.SaveChanges();
                return RedirectToAction(nameof(List));
            }

            complaint.User = _context.Users
                .FirstOrDefault(u => u.UserId == complaint.UserId);

            return View(complaint);
        }
    }
}


















//using ComplaintManagementSystem.Data;
//using ComplaintManagementSystem.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;

//namespace ComplaintManagementSystem.Controllers
//{
//    public class ComplaintController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public ComplaintController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // Register Complaint Page
//        public IActionResult Create()
//        {
//            ViewBag.UserId = new SelectList(_context.Users, "UserId", "Name");
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Create(Complaint complaint)
//        {
//            complaint.Status = "Pending";
//            complaint.CreatedDate = DateTime.Now;

//            _context.Complaints.Add(complaint);
//            _context.SaveChanges();

//            return RedirectToAction("List");
//        }

//        // Complaint List
//        public IActionResult List()
//        {
//            var complaints = _context.Complaints
//                .Include(c => c.User)
//               .ToList();

//            return View(complaints);
//        }

//        public IActionResult Details(int id)
//        {
//            var complaint = _context.Complaints
//                .Include(c => c.User)
//                .FirstOrDefault(c => c.ComplaintId == id);

//            if (complaint == null)
//                return NotFound();

//            return View(complaint);
//        }

//        public IActionResult Delete(int id)
//        {
//            var complaint = _context.Complaints
//                .Include(c => c.User)
//                .FirstOrDefault(c => c.ComplaintId == id);

//            if (complaint == null)
//                return NotFound();

//            return View(complaint);
//        }

//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public IActionResult DeleteConfirmed(int id)
//        {
//            var complaint = _context.Complaints.Find(id);

//            if (complaint != null)
//            {
//                _context.Complaints.Remove(complaint);
//                _context.SaveChanges();
//            }

//            return RedirectToAction(nameof(Dashboard));
//        }


//        public IActionResult UpdateStatus(int id)
//        {
//            var complaint = _context.Complaints.Find(id);
//            _context.Complaints.Update(complaint);
//            _context.SaveChanges();
//            return View(complaint);
//        }

//        [HttpPost]
//        public IActionResult UpdateStatus(Complaint complaint)
//        {

//            return RedirectToAction("List");
//        }

//    }
//}
