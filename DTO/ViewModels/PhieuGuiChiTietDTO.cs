using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModels
{
    public class PhieuGuiChiTietDTO
    {
        public string MaPhieu { get; set; }
        public string TenThuCung { get; set; } // Lấy từ bảng ThuCung
        public string TenKhachHang { get; set; } // Lấy từ bảng KhachHang
        public DateTime NgayNhan { get; set; }
        public DateTime NgayTraDuKien { get; set; }
        public string TinhTrangLucNhan { get; set; }
    }
}
