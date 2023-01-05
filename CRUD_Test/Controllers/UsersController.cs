using CRUD_Test.Data;
using CRUD_Test.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Test.Controllers
{
    public class UsersController : Controller
    {
        ApplicationDbContext context;
        public UsersController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await context.Users.ToListAsync();
            return View(users);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (await context.Users.FirstOrDefaultAsync(p => p.Login == user.Login) != null)
            {
                return View("This user already exist!");
            }
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return RedirectToAction("List");
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            User user = await context.Users.FirstOrDefaultAsync(p => p.UserID == id);
            if (user != null)
            {
                return View(user);
            }
            return NotFound();
        }
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            User user = await context.Users.FirstOrDefaultAsync(p => p.UserID == id);
            Order order = await context.Orders.FirstOrDefaultAsync(p => p.UserId == user.UserID);
            if (order!=null)
            {
                return RedirectToAction("List");
            }
            if (user != null)
            {
                return View(user);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                User user = await context.Users.FirstOrDefaultAsync(p => p.UserID == id);
                
                context.Users.Remove(user);
                await context.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return NotFound();
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            User user = context.Users.Find(id);
            if (user != null)
            {
                return View(user);
            }
            return NotFound();
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            context.Entry(user).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
