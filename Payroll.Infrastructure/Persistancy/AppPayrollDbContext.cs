using Microsoft.EntityFrameworkCore;
using Payroll.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Infrastructure.Persistancy
{
    public class AppPayrollDbContext : DbContext
    {
        public AppPayrollDbContext(DbContextOptions<AppPayrollDbContext> options) : base(options) { }

        public DbSet<PayrollEntity> Payrolls { get; set; }

    }
}
