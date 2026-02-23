using DAL.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class DatLichDAL
    {
        DataProvider db = new DataProvider();

        public DataTable GetAll()
        {
            return db.ExecuteQuery("SELECT * FROM DatLich");
        }

        public bool Insert(string maDL, string maKH, System.DateTime ngayDat)
        {
            string query = "INSERT INTO DatLich (MaDatLich, MaKhachHang, NgayDat) VALUES ( @ma , @makh , @ngay )";
            return db.ExecuteNonQuery(query, new object[] { maDL, maKH, ngayDat }) > 0;
        }
    }
}
