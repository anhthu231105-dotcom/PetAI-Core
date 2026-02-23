using DAL.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class TaiKhoanDAL
    {
        DataProvider db = new DataProvider();

        public DataTable Login(string user, string pass)
        {
            string query = "SELECT * FROM TaiKhoan WHERE TenDangNhap = @user AND MatKhau = @pass AND TrangThai = 1";
            return db.ExecuteQuery(query, new object[] { user, pass });
        }
    }
}

