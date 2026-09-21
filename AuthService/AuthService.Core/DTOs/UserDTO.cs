using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Core.DTOs
{
    public class UserDTO
    {
     
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; } 

        public string? Role { get; set; }
    }
}
