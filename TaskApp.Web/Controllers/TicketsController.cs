using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskApp.Web.Data;
using TaskApp.Web.Models;

namespace TaskApp.Web.Controllers
{
    public class TicketsController : Controller
    {

        public readonly ApplicationDbContext _context;

        public TicketsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Ticket> tasks = _context.Tickets.OrderByDescending(t => t.Id).ToList();
            return View(tasks);
        }

        //Get Create
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(Ticket task)
        {

            if (task.Title is int) 
            {
                ModelState.AddModelError("Title","Invalid Task Title");
            }

            if (task.Title == task.Description.ToString())
            {
                ModelState.AddModelError("Description", "Title and Description Both Can not be Same");
            }
            if (ModelState.IsValid)
            {
                var username = User.Identity.Name;
                task.User = username;
                _context.Tickets.Add(task);
                _context.SaveChanges();
                TempData["success_notification"] = "Task Added Successfully";
            }
            return RedirectToAction("Index");
        }

        //Edit Get
        public IActionResult Edit(int? id)
        {
            if (id == null) 
            { 
            return NotFound();
            }

            Ticket task = _context.Tickets.Where(t => t.Id == id).FirstOrDefault();

            if (task == null)
            {
                return NotFound();            
            }
            return View(task);
        }


        [HttpPost]
        public IActionResult Edit(Ticket task)
        {

            if (task.Title is int)
            {
                ModelState.AddModelError("Title", "Invalid Task Title");
            }

            if (task.Title == task.Description.ToString())
            {
                ModelState.AddModelError("Description", "Title and Description Both Can not be Same");
            }
            if (ModelState.IsValid)
            {
                var username = User.Identity.Name;
                task.User = username;
                _context.Tickets.Update(task);
                _context.SaveChanges();
                TempData["update_notification"] = "Task Updated Successfully";
            }
            return RedirectToAction("Index");
        }


        //Delete Get
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ticket task = _context.Tickets.Where(t => t.Id == id).FirstOrDefault();

            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }


        [HttpPost , ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Ticket task = _context.Tickets.Where(t => t.Id == id).FirstOrDefault();

            if (task == null) 
            {
                NotFound();
            }

                _context.Tickets.Remove(task);
                _context.SaveChanges();
                TempData["delete_notofication"] = "Task Deleted Successfully";
                return RedirectToAction("Index");
        }



        //Task Details Get
        public IActionResult Details(int? id) 
        {
            if (id == null) 
            {
                return NotFound();
            
            }

            var tasks = _context.Tickets.Where(t => t.Id == id).FirstOrDefault();

            if (tasks == null) 
            { 
            
            return NotFound();
            
            }

            return View(tasks);

        }





    }
}
