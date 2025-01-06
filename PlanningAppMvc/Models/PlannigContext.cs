using Microsoft.EntityFrameworkCore;

namespace PlanningAppMvc.Models
{
    public class PlannigContext : DbContext
    {
        public PlannigContext(DbContextOptions<PlannigContext> options) : base(options)
        {

        }


        public DbSet<Plan> Plans { get; set; }
    }
}
