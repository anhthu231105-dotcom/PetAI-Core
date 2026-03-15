using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DTO.AI_Models
{
    public class HealthPredictionDTO
    {
        public int RecordID { get; set; }
        public string PetID { get; set; }
        // 1. CÁC THUỘC TÍNH ĐỂ NHẬN DỮ LIỆU TỪ PYTHON (Phải có cái này mới hiện được chữ)
        public string Result { get; set; }   // Để nhận "Cảnh báo nguy hiểm" hoặc "Bình thường"
        public string Advice { get; set; }   // Để nhận lời khuyên chi tiết từ AI
        public float Confidence { get; set; } // Để nhận con số 95.5%

        // 2. CÁC THÔNG SỐ ĐẦU VÀO ĐỂ LƯU XUỐNG SQL
        public double Temperature { get; set; }
        public int HeartRate { get; set; }
        public string DiagnosisID { get; set; }
        public DateTime PredictionDate { get; set; }
       /* public float Confidence { get; set; }
        public double Temperature { get; set; }
        public int HeartRate { get; set; }
        public string DiagnosisID { get; set; } // Liên kết tới mã bệnh lý
        public DateTime PredictionDate { get; set; }*/

    }
}
