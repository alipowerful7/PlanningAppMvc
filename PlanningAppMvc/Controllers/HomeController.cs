using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanningAppMvc.Models;

namespace PlanningAppMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PlannigContext _context;
        public HomeController(ILogger<HomeController> logger, PlannigContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            DateTime d = DateTime.Now;
            d = d.AddDays(3);
            var plan = await _context.Plans.Where(p => p.IsDone == false && (DateTime.Now.Date <= p.DoneDate && p.DoneDate <= d)).ToListAsync();
            plan = plan.OrderByDescending(p => p.DoneDate).Reverse().ToList();
            return View(plan);
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
    }
}
