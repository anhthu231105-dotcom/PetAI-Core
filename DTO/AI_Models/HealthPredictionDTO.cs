using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.AI_Models
{
    public class HealthPredictionDTO
    {
        public string MaThuCung { get; set; }
        public double DiemSucKhoe { get; set; } // Ví dụ: 0.8 (80%)
        public string KetLuanAI { get; set; }    // Ví dụ: "Có dấu hiệu stress"
        public string CanhBao { get; set; }      // Ví dụ: "Cần đi khám bác sĩ"
        public List<string> DichVuGoiY { get; set; } // AI gợi ý các dịch vụ phù hợp
    }
}
