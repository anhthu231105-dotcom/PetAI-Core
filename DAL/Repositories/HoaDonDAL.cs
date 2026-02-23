using DAL.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class HoaDonDAL
    {
        DataProvider db = new DataProvider();

        public bool CreateInvoice(string maHD, string maKH, string maPet, decimal tongTien)
        {
            string query = "INSERT INTO HoaDon (MaHoaDon, MaKhachHang, MaThuCung, NgayLap, TongTien) VALUES ( @maHD , @maKH , @maPet , GETDATE() , @tien )";
            return db.ExecuteNonQuery(query, new object[] { maHD, maKH, maPet, tongTien }) > 0;
        }

        public DataTable GetRevenueByMonth(int month, int year)
        {
            string query = "SELECT * FROM HoaDon WHERE MONTH(NgayLap) = @m AND YEAR(NgayLap) = @y";
            return db.ExecuteQuery(query, new object[] { month, year });
        }
    }
}
