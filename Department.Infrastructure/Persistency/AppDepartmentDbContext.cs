using Microsoft.EntityFrameworkCore;
using DepartmentCore.Core.Entities;

namespace Department.Infrastructure.Persistency
{
    public class AppDepartmentDbContext : DbContext
    {
        public AppDepartmentDbContext(DbContextOptions<AppDepartmentDbContext> options) : base(options) { }

        public DbSet<DepartmentEntity> Departments { get; set; }
    }
}

