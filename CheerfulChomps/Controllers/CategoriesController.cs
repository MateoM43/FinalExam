using CheerfulChomps.Data;
using CheerfulChomps.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CheerfulChomps.Controllers
{
    //[Authorize] // authentication check: block anonymous users
    [Authorize(Roles = "Administrator")]
    public class CategoriesController : Controller
    {
        // shared db connection obj
        ApplicationDbContext _context;

        // constructor: every time controller instance created, pass in a db connection obj
        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // fetch categories from db using DbSet
            var categories = _context.Category.OrderBy(c => c.Name).ToList();

            // pass list to view for display
            return View("Index", categories);
        }

        // GET: /Categories/Create => show empty Category form
        public IActionResult Create()
        {
            return View("Create");
        }

        // POST: /Categories/Create => process form submission 
        [HttpPost]
        public IActionResult Create([Bind("Name")] Category category)
        {
            // validate input
            if (!ModelState.IsValid)
            {
                // show user the form again to fix their mistakes
                return View("Create");
            }

            // save to db using DbSet object
            _context.Category.Add(category);
            _context.SaveChanges();

            // redirect to updated list on Index
            return RedirectToAction("Index");
        }

        // GET: /Categories/Edit/27 => look up Category based on id param so user can Edit it
        public IActionResult Edit(int id)
        {
            // try to find selected Category to populate form
            var category = _context.Category.Find(id);

            if (category == null)
            {
                return View("404");
            }

            // pass category to view for display.  specify view name for clarity
            return View("Edit", category);
        }

        // POST: /Categories/Edit/27 => update Category and redirect to list
        [HttpPost]
        public IActionResult Edit([Bind("CategoryId,Name")] Category category)
        {
            // validate
            if (!ModelState.IsValid)
            {
                return View();
            }

            // save to db
            _context.Category.Update(category);
            _context.SaveChanges();

            // redirect
            return RedirectToAction("Index");
        }

        // GET: /Categories/Delete/27 => delete selected Category and refresh list
        public IActionResult Delete(int id)
        {
            var category = _context.Category.Find(id);

            if (category == null)
            {
                return View("404");
            }

            // remove from db
            _context.Category.Remove(category);
            _context.SaveChanges();

            // redirect
            return RedirectToAction("Index");
        }
    }
}
