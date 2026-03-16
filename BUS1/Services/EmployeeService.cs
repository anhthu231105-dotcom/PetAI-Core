using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Services
{
    public class EmployeeService
    {
        private readonly UserRepository _userRepo = new UserRepository();

        public bool Login(string username, string password)
        {
            // Ở đây có thể thêm logic mã hóa MD5/SHA cho password trước khi gửi xuống DAL
            return _userRepo.Login(username, password);
        }
    }
}
