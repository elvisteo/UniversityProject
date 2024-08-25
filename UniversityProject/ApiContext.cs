using Microsoft.EntityFrameworkCore;
using UniversityProject.Models;

namespace UniversityProject
{
    public class ApiContext : DbContext
    {
        public DbSet<University> Universities { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
    }
}
