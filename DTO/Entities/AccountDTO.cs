using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Entities
{
    public class AccountDTO//Tài khoản
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmployeeID { get; set; }
        public string Role { get; set; } // Admin hoặc Staff
    }
}
