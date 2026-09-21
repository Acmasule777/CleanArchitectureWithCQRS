using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Interfaces
{
    public interface IPasswordHash
    {
        string hashPassword(string password);
        bool verifyPassword(string password, string hashPassword);
    }
}
