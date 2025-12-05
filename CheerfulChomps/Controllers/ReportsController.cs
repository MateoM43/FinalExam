using CheerfulChomps.Data;
using CheerfulChomps.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CheerfulChomps.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ReportsController : Controller
    {
        // shared db connection
        ApplicationDbContext _context;

        // constructor w/db conn dependency
        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Product
                .Include(r => r.Category)
                .OrderBy(r => r.Name)
                .ToList();

            return View(Reports);
        }

    }
}