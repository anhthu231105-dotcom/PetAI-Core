using DAL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PhieuNhapHangDAL
    {
        DataProvider db = new DataProvider();

        public bool Insert(string maPN, string maNCC, decimal tongTien)
        {
            string query = "INSERT INTO PhieuNhapHang (MaPhieuNhap, MaNhaCungCap, NgayNhap, TongTien) " +
                           "VALUES ( @ma , @ncc , GETDATE() , @tien )";
            return db.ExecuteNonQuery(query, new object[] { maPN, maNCC, tongTien }) > 0;
        }
    }
}
