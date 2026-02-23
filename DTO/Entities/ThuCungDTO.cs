using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Entities
{
    public class ThuCungDTO
    {
        public string MaThuCung { get; set; }
        public string TenThuCung { get; set; }
        public string Loai { get; set; }
        public string Giong { get; set; }
        public int? Tuoi { get; set; }
        public double? CanNang { get; set; }
        public string MaKhachHang { get; set; }
    }
}
