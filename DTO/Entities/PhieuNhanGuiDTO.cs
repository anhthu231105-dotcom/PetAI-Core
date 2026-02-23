using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Entities
{
    public class PhieuNhanGuiDTO
    {
        public string MaPhieu { get; set; }
        public string MaKhachHang { get; set; }
        public string MaThuCung { get; set; }
        public DateTime NgayNhan { get; set; }
        public DateTime? NgayTraDuKien { get; set; }
        public string MaDatLich { get; set; }
    }
}
