using DAL.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ThuCungDAL
    {
        DataProvider db = new DataProvider();

        public DataTable GetAll()
        {
            return db.ExecuteQuery("SELECT * FROM ThuCung");
        }

        public bool Insert(string ma, string ten, string loai, string giong, int tuoi, double cannang, string makh)
        {
            string query = "INSERT INTO ThuCung (MaThuCung, TenThuCung, Loai, Giong, Tuoi, CanNang, MaKhachHang) VALUES ( @ma , @ten , @loai , @giong , @tuoi , @nang , @makh )";
            return db.ExecuteNonQuery(query, new object[] { ma, ten, loai, giong, tuoi, cannang, makh }) > 0;
        }

        public bool Update(string ma, string ten, double cannang)
        {
            string query = "UPDATE ThuCung SET TenThuCung = @ten , CanNang = @nang WHERE MaThuCung = @ma";
            return db.ExecuteNonQuery(query, new object[] { ten, cannang, ma }) > 0;
        }
    }
}
