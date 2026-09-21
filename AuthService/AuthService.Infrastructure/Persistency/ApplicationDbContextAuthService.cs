using AuthService.Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.Persistency
{
    public class ApplicationDbContextAuthService : DbContext
    {
        public ApplicationDbContextAuthService(DbContextOptions<ApplicationDbContextAuthService> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
