using DAL.Repositories;
using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Transactions
{
    public class HoaDonBUS
    {
        private HoaDonDAL dal = new HoaDonDAL();

        public bool ThanhToanHoaDon(HoaDonDTO hoaDon)
        {
            // Logic: Tổng tiền phải lớn hơn 0 mới cho phép lưu
            if (hoaDon.TongTien <= 0) return false;

            return dal.CreateInvoice(hoaDon.MaHoaDon, hoaDon.MaKhachHang, hoaDon.MaThuCung, hoaDon.TongTien);
        }

        public DataTable LayDoanhThuTheoThang(int thang, int nam)
        {
            return dal.GetRevenueByMonth(thang, nam);
        }
    }
}
