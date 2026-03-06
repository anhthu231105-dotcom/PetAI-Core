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
        public double Temperature { get; set; }
        public int HeartRate { get; set; }
        public string DiagnosisID { get; set; } // Liên kết tới mã bệnh lý
        public DateTime PredictionDate { get; set; }
    }
}
