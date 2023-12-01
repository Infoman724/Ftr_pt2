using Ftr_pt2.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Ftr_pt2.Data
{
    public class AppDbContext : DbContext
    {


        public DbSet<Employee> Employees { get; set; }
        public DbSet<WorkTime> WorkTimes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}