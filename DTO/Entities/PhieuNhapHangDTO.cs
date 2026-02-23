using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Entities
{
    public class PhieuNhapHangDTO
    {
        public string MaPhieuNhap { get; set; }
        public string MaNhaCungCap { get; set; }
        public DateTime NgayNhap { get; set; }
        public decimal TongTien { get; set; }
    }
}
