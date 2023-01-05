using CRUD_Test.Data;
using CRUD_Test.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Test.Controllers
{
    public class OrdersController : Controller
    {
        ApplicationDbContext context;
        public OrdersController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var orders = context.Orders.Include(p => p.User);

            return View(orders.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            SelectList users = new SelectList(context.Users, "UserID", "Login");
            ViewBag.Users = users;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            var orders = context.Orders.Where(p => p.OrderDate == DateTime.Today);
            orders = orders.Where(p => p.UserId == order.UserId);
            if (orders.AsEnumerable().Any())
            {
                return RedirectToAction("List");
            }
            var users = context.Users.Where(p => p.UserID == order.UserId);
            if (!users.AsEnumerable().Any())
            {
                return RedirectToAction("List");
            }
            order.OrderDate = DateTime.Today;
            context.Orders.Add(order);
            await context.SaveChangesAsync();
            return RedirectToAction("List");
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Order order = context.Orders.Find(id);
            if (order != null)
            {
                order.User = context.Users.Find(order.UserId);
                return View(order);
            }
            return NotFound();
        }
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            Order order = context.Orders.Find(id);
            if (order != null)
            {
                return View(order);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Order order = await context.Orders.FirstOrDefaultAsync(p => p.OrderID == id);
                context.Orders.Remove(order);
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
            Order order = context.Orders.Find(id);
            if (order != null)
            {
                SelectList users = new SelectList(context.Users, "UserID", "Login", order.UserId);
                ViewBag.Users = users;
                return View(order);
            }
            return NotFound();
        }
        [HttpPost]
        public ActionResult Edit(Order order)
        {
            context.Entry(order).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("List");
        }
    }
}
