using ComplaintManagementSystem.Data;
using ComplaintManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;

namespace ComplaintManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _client;

        private readonly string url = "https://localhost:7275/api/UsersApi/";

        // ✅ SINGLE constructor ONLY
        public UserController(ApplicationDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _client = httpClientFactory.CreateClient();
        }

        // Show Register Form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Save User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User users)
        {
            string data = JsonConvert.SerializeObject(users);
            StringContent content =
                new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response =
                _client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                TempData["insert_message"] =
                    "Registration successful. You can now register a complaint.";

                return RedirectToAction("Create", "Complaint");
            }

            return View(users);
        }
    }
}
















//using ComplaintManagementSystem.Data;
//using ComplaintManagementSystem.Models;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using System.Text;
//using System.Net.Http;

//namespace ComplaintManagementSystem.Controllers
//{
//    public class UserController : Controller
//    {
//        private readonly ApplicationDbContext _context;
//        private string url = "https://localhost:7275/api/UsersApi/";
//        private HttpClient client = new HttpClient();

//        //public UserController(ApplicationDbContext context)
//        //{
//        //    _context = context;
//        //}

//        // Show Register Form
//        public IActionResult Create()
//        {
//            return View();
//        }


//        // Save User
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public IActionResult Create(User users)
//        {
//            string data = JsonConvert.SerializeObject(users);
//            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
//            HttpResponseMessage response = client.PostAsync(url, content).Result;
//            if (response.IsSuccessStatusCode)
//            {
//                TempData["insert_message"] = "Registeration Success on you can Register complaint";
//                return RedirectToAction("Complaint");
//            }
//            return View();
//        }
//    }
//}

