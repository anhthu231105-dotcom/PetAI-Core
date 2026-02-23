using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Entities
{
    public class HoaDonDTO
    {
        public string MaHoaDon { get; set; }
        public string MaKhachHang { get; set; }
        public string MaThuCung { get; set; }
        public DateTime NgayLap { get; set; }
        public decimal TongTien { get; set; }
    }
}
