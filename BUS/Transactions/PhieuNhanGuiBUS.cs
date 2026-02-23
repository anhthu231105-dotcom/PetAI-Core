using DAL.Repositories;
using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Transactions
{
    public class PhieuNhanGuiBUS
    {
        private PhieuNhanGuiDAL dal = new PhieuNhanGuiDAL();

        public bool LapPhieuGui(PhieuNhanGuiDTO phieu)
        {
            // Logic: Ngày trả dự kiến phải sau ngày nhận
            if (phieu.NgayTraDuKien < phieu.NgayNhan) return false;

            return dal.Insert(
                phieu.MaPhieu,
                phieu.MaKhachHang,
                phieu.MaThuCung,
                phieu.NgayNhan,
                phieu.NgayTraDuKien,
                phieu.MaDatLich // Pass MaDatLich as required by Insert signature
            );
        }
    }
}