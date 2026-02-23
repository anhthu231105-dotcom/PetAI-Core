using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories;

namespace BUS.Security
{
    public class TaiKhoanBUS
    {
        private TaiKhoanDAL dal = new TaiKhoanDAL();

        public bool DangNhap(string username, string password)
        {
            // Logic: Không cho phép để trống trường nhập liệu
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return false;

            // Gọi DAL để kiểm tra trong Database
            DataTable dt = dal.Login(username, password);
            return dt.Rows.Count > 0;
        }
    }
}
