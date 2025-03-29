using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prj.TaskManager.Data;
using Prj.TaskManager.Models;

namespace Prj.TaskManager.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        readonly AppDbContext _context;
        public TaskController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        [HttpGet]
        public  IActionResult Index()
        {
           var data =   _context.Tasks.ToList();
            return View(data);
            
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskItemModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Tasks.Add(model);
                var result = await _context.SaveChangesAsync();
                TempData["Message"] = "Task Added";
                return RedirectToAction("Index");

            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var task = _context.Tasks.SingleOrDefault(  x => x.Id == id);
            return View(task);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var task = _context.Tasks.SingleOrDefault(x => x.Id == id);
            return View(task);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var task = _context.Tasks.SingleOrDefault(x => x.Id == id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
                TempData["Alert"] = "Deleted Successfully";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var task = _context.Tasks.SingleOrDefault(x => x.Id == id);
            return View(task);
        }
        [HttpPost]
        public IActionResult Edit(int id,TaskItemModel model)
        {
            if (id != model.Id ||ModelState.IsValid==false)
            {
                return View(model);
            }
            _context.Tasks.Update(model);   
            _context.SaveChanges();
            TempData["Message"] = "task updated";
            return RedirectToAction("Index");
        }

    }
}
