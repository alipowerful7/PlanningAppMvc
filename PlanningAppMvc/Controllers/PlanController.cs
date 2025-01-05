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
    }
}
