using DAL.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class NhatKyDAL
    {
        DataProvider db = new DataProvider();

        public DataTable GetHealthHistory(string maThuCung)
        {
            string query = "SELECT * FROM NhatKy WHERE MaThuCung = @ma ORDER BY NgayCapNhat DESC";
            return db.ExecuteQuery(query, new object[] { maThuCung });
        }

        public bool InsertLog(string maPet, string tinhTrang, string ghiChu)
        {
            string query = "INSERT INTO NhatKy (MaThuCung, NgayCapNhat, TinhTrangSucKhoe, GhiChu) VALUES ( @ma , GETDATE() , @tt , @gc )";
            return db.ExecuteNonQuery(query, new object[] { maPet, tinhTrang, ghiChu }) > 0;
        }
}
}
