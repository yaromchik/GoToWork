using GoToWork_DataAccess;
using GoToWork_Models;
using GoToWork_Models.Models.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GoToWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
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

      
        [Authorize(Roles = "Admin")]
        public IActionResult Moderation()
        {
            List<UserVM> users=new List<UserVM>();
            foreach (var userVM in _db.Users)
            {
                var roleId = _db.UserRoles.Where(c => c.UserId == userVM.Id).FirstOrDefault().RoleId;
                var roleName = _db.Roles.Where(c => c.Id == roleId).FirstOrDefault().Name;
                UserVM tempUser = new UserVM()
                {
                    RoleName = roleName,
                    Users=userVM
                };
                users.Add(tempUser);
            }
            
            return View(users);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id) 
        {
            var user=_db.Users.Find(id);
            if (user != null)
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
                return RedirectToAction("Moderation");
            }
            return NotFound();
        }
    }
}
