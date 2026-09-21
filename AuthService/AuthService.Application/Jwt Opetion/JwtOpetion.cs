using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Jwt_Opetion
{
    public class JwtOpetion
    {
        public const string Section = "Jwt";
        public string Key { get; set; }

        public string Issuer { get; set; } = string.Empty;

        public string Audience { get; set; } = string.Empty;

        public int ExpiryMinutes { get; set; }
    }
}
