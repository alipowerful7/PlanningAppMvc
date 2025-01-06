using Microsoft.AspNetCore.Mvc;
using PlanningAppMvc.Models;

namespace PlanningAppMvc.Controllers
{
    public class PlanController : Controller
    {
        private readonly PlannigContext _context;
        public PlanController(PlannigContext context)
        {
            _context = context;
        }
        public IActionResult Creat()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatPost(Plan plan)
        {
            plan.CreatDate = DateTime.Now;
            TempData["CreatError"] = null;
            if (plan.DoneDate.Date < DateTime.Now.Date)
            {
                TempData["CreatError"] = "Done date must be greater than or equal to today";
                return Redirect("/Plan/Creat");
            }
            if (ModelState.IsValid)
            {
                await _context.Plans.AddAsync(plan);
                await _context.SaveChangesAsync();
            }
            return Redirect("/");
        }
    }
}
