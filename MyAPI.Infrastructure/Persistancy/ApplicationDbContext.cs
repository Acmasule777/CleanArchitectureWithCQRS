using Microsoft.EntityFrameworkCore;
using MyAPI.Core.Entities;

namespace MyAPI.Infrastructure.Persistancy
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Employee> Employees {  get; set; }
    }
}
