using DAL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository
    {
        public bool Login(string email, string password)
        {
            // Lưu ý: Trong thực tế nên mã hóa password, đây là ví dụ cơ bản
            string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email AND Password = @Password";
            object[] parameter = { email, password };

            int result = (int)DataProvider.Instance.ExecuteScalar(query, parameter);
            return result > 0;
        }
    }
}
