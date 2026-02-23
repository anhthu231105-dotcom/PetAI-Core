using DAL.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class KhachHangDAL
    {
        DataProvider db = new DataProvider();

        public DataTable GetAll() => db.ExecuteQuery("SELECT * FROM KhachHang");

        public DataTable SearchByName(string name)
        {
            string query = "SELECT * FROM KhachHang WHERE Ten LIKE '%' + @name + '%'";
            return db.ExecuteQuery(query, new object[] { name });
        }
    }
}
