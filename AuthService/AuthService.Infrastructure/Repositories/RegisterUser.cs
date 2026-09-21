using AuthService.Application.Interfaces;
using AuthService.Core.DTOs;
using AuthService.Core.Entity;
using AuthService.Infrastructure.Persistency;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.Repositories
{
    public class RegisterUser : IUserRepository
    {
        private readonly ApplicationDbContextAuthService _context;

        public RegisterUser(ApplicationDbContextAuthService context)
        {
            _context = context;
        }

        public async Task<UserDTO> FindUserByUsername(string username)
        {
            var user = await _context.Users.Select(u => new UserDTO
            {
                Id = u.Id,
                Username = u.Username,
                PasswordHash = u.PasswordHash,
                Role = u.Role,
            })
            .Where(u => u.Username == username)
            .FirstOrDefaultAsync();

            return user;

        }

        public async Task<string> registerUser(User user, CancellationToken cancellation)
        {
            await _context.Users.AddAsync(new User
            {
                Username = user.Username,
                PasswordHash = user.PasswordHash,
                Role = user.Role
            }, cancellation);

            await _context.SaveChangesAsync();

            return "User registered successfully";
        }
    }
}
 