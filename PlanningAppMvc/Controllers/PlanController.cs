﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Index(string? mode = "All")
        {
            var plan = await _context.Plans.ToListAsync();
            if (mode == null || mode == "All")
            {
                plan = await _context.Plans.Where(p => p.DoneDate >= DateTime.Now.Date || p.IsDone == true).ToListAsync();
            }
            else if (mode == "Done")
            {
                plan = await _context.Plans.Where(p => p.IsDone == true && p.DoneDate >= DateTime.Now.Date).ToListAsync();
            }
            else if (mode == "NotDone")
            {
                plan = await _context.Plans.Where(p => p.IsDone == false && p.DoneDate >= DateTime.Now.Date).ToListAsync();
            }
            else
            {
                plan = await _context.Plans.Where(p => p.DoneDate >= DateTime.Now.Date).ToListAsync();
            }
            plan = plan.OrderByDescending(p => p.DoneDate).Reverse().ToList();
            return View(plan);
        }
        public IActionResult Creat()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatPost(Plan plan)
        {
            plan.CreatDate = DateTime.Now;
            plan.DoneDate = plan.DoneDate.ToMiladi();
            TempData["CreatError"] = null;
            if (plan.DoneDate.Date < DateTime.Now.Date)
            {
                TempData["CreatError"] = "Done date must be greater than or equal to today";
                return Redirect("/Plan/Creat");
            }
            await _context.Plans.AddAsync(plan);
            await _context.SaveChangesAsync();
            return Redirect("/");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var plan = await _context.Plans.FindAsync(id);
            if (plan == null)
            {
                return NotFound();
            }
            _context.Plans.Remove(plan);
            await _context.SaveChangesAsync();
            return Redirect("/Plan/Index");
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var plan = await _context.Plans.FindAsync(id);
            return View(plan);
        }
        [HttpPost]
        public async Task<IActionResult> EditPost(Plan plan)
        {
            var planToUpdate = await _context.Plans.FindAsync(plan.Id);
            if (planToUpdate == null)
            {
                return NotFound();
            }
            planToUpdate.Title = plan.Title;
            planToUpdate.Description = plan.Description;
            planToUpdate.DoneDate = plan.DoneDate.ToMiladi();
            _context.Plans.Update(planToUpdate);
            await _context.SaveChangesAsync();
            return Redirect("/");
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var plan = await _context.Plans.FindAsync(id);
            return View(plan);
        }
        public async Task<IActionResult> DonePlan(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var plan = await _context.Plans.FindAsync(id);
            if (plan == null)
            {
                return NotFound();
            }
            plan.IsDone = true;
            await _context.SaveChangesAsync();
            return Redirect("/Plan/Index");
        }
        public async Task<IActionResult> Search(string? search)
        {
            var plan = await _context.Plans.Where(p => p.Title!.Contains(search!)).ToListAsync();
            ViewData["SearchValue"] = search;
            return View(viewName: "Index", model: plan);
        }
    }
}
