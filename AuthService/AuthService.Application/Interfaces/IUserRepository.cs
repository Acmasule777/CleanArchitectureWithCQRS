using AuthService.Core.DTOs;
using AuthService.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<string> registerUser(User user, CancellationToken cancellation);
        Task<UserDTO> FindUserByUsername(string username);
    }
}
