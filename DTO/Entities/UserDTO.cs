using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Entities
{
    public class UserDTO//Tài khoản
    {
        // 1. Thêm UserID để làm khóa chính (khớp với SQL)
        public string UserID { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        // Gán giá trị mặc định để tránh lỗi Null sau này
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty; // Admin hoặc Staff

        public string Email { get; set; } = string.Empty;

        public string EmployeeID { get; set; } = string.Empty;
    }
}
