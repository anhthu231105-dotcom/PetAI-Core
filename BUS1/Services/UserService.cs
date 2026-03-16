using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Services
{
    public class UserService
    {
        // Khởi tạo Repository để làm việc với Database
        private readonly UserRepository _userRepo = new UserRepository();

        /// <summary>
        /// Hàm kiểm tra đăng nhập từ Database
        /// </summary>
        public bool Authenticate(string username, string password)
        {
            // Kiểm tra cơ bản trước khi xuống Database (Logic của tầng BUS)
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            // Gọi xuống DAL để truy vấn bảng Account
            // Lưu ý: UserRepository phải có hàm Login nhận 2 tham số này
            return _userRepo.Login(username, password);
        }

        // Thư có thể thêm hàm đổi mật khẩu hoặc tạo tài khoản mới ở đây
    }
}
