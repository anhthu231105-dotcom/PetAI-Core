using DAL.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PhieuNhanGuiDAL
    {
        DataProvider db = new DataProvider();

        public DataTable GetAll()
        {
            return db.ExecuteQuery("SELECT * FROM PhieuNhanGui");
        }

        public bool Insert(string maP, string maKH, string maPet, System.DateTime ngayNhan, System.DateTime? ngayTra, string maDL)
        {
            string query = "INSERT INTO PhieuNhanGui (MaPhieu, MaKhachHang, MaThuCung, NgayNhan, NgayTraDuKien, MaDatLich) " +
                           "VALUES ( @maP , @maKH , @maPet , @nNhan , @nTra , @maDL )";
            return db.ExecuteNonQuery(query, new object[] { maP, maKH, maPet, ngayNhan, ngayTra, maDL }) > 0;
        }
    }
}
