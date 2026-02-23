using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Entities
{
    public class NhatKyDTO
    {
        public int MaNhatKy { get; set; }
        public string MaThuCung { get; set; }
        public DateTime NgayCapNhat { get; set; }
        public string TinhTrangSucKhoe { get; set; }
        public string GhiChu { get; set; }
    }
}
