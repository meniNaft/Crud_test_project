using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Crud_test_project.Models;
using Crud_test_project.Services;

namespace Crud_test_project.Controllers
{
    public class ToDoModelsController : Controller
    {
        private readonly ApiToDoService _context;

        public ToDoModelsController(ApiToDoService context)
        {
            _context = context;
        }

        // GET: ToDoModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.GetToDosAsync());
        }

        // GET: ToDoModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoModel = await _context.GetToDoByIdAsync(id.Value);
            if (toDoModel == null)
            {
                return NotFound();
            }

            return View(toDoModel);
        }

        // GET: ToDoModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ToDoModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ToDo,Completed,UserId")] ToDoModel toDoModel)
        {
            if (ModelState.IsValid)
            {
                var res = await _context.CreateToDoAsync(toDoModel);
                return RedirectToAction(nameof(Index));
            }
            return View(toDoModel);
        }

        // GET: ToDoModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoModel = await _context.GetToDoByIdAsync(id.Value);
            if (toDoModel == null)
            {
                return NotFound();
            }
            return View(toDoModel);
        }

        // POST: ToDoModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ToDo,Completed,UserId")] ToDoModel toDoModel)
        {
            if (id != toDoModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _context.UpdateToDoAsync(toDoModel.id, toDoModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ToDoModelExists(toDoModel.id))
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
            return View(toDoModel);
        }

        // GET: ToDoModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoModel = await _context.GetToDoByIdAsync(id.Value);
            if (toDoModel == null)
            {
                return NotFound();
            }

            return View(toDoModel);
        }

        // POST: ToDoModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _context.DeleteToDoAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ToDoModelExists(int id)
        {
            return await _context.GetToDoByIdAsync(id) != null;
        }
    }

}
