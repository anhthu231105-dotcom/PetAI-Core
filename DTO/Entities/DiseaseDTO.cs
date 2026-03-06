using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Entities
{
    public class DiseaseDTO//Bệnh lý
    {
        public string DiseaseID { get; set; }
        public string DiseaseName { get; set; }
        public string MainSymptom { get; set; }   // Triệu chứng chính
        public string MedicalAdvice { get; set; } // Lời khuyên y tế
        public string DangerLevel { get; set; }   // Mức độ nguy hiểm
    }
}