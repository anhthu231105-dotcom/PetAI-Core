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
            try
            {
                // Câu truy vấn chuẩn với bảng Users và cột Email
                string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email AND Password = @Password";

                object[] parameter = new object[] { email, password };

                // Thực thi lệnh
                int result = (int)DataProvider.Instance.ExecuteScalar(query, parameter);

                return result > 0;
            }
            catch (Exception ex)
            {
                // Xóa MessageBox ở đây để hết lỗi đỏ CS0234
                throw new Exception("Lỗi truy vấn tại UserRepository: " + ex.Message);
            }
 
}
}
}
