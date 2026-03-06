using DAL.Core;
using DTO.AI_Models; // Gọi đến DTO trong folder AI_Models
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AI_Models
{
    public class HealthPredictionDAL
    {
        private static HealthPredictionDAL instance;
        public static HealthPredictionDAL Instance
        {
            get { if (instance == null) instance = new HealthPredictionDAL(); return instance; }
            private set => instance = value;
        }

        private HealthPredictionDAL() { }

        // Hàm lưu kết quả dự đoán từ AI vào bảng HealthRecord
        public bool SavePrediction(HealthPredictionDTO prediction)
        {
            // Câu lệnh SQL để lưu thông tin sức khỏe
            string query = "INSERT INTO HealthRecord (PetID, Temperature, HeartRate, DiagnosisID, PredictionDate) " +
                           "VALUES ( @PetID , @Temp , @Heart , @DiagID , GETDATE() )";

            // Truyền tham số vào để tránh lỗi SQL Injection
            object[] parameter = new object[] {
                prediction.PetID,
                prediction.Temperature,
                prediction.HeartRate,
                prediction.DiagnosisID
            };

            int result = DataProvider.Instance.ExecuteNonQuery(query, parameter);

            return result > 0; // Trả về true nếu lưu thành công
        }

        // Hàm lấy kết quả dự đoán mới nhất của một bé Pet
        public DataTable GetLatestPrediction(string petID)
        {
            string query = "SELECT TOP 1 * FROM HealthRecord WHERE PetID = @PetID ORDER BY PredictionDate DESC";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { petID });
        }

        // Hàm dự đoán sức khỏe dựa trên nhiệt độ và nhịp tim
        public double Predict(double temp, int heart)
        {
            // Ví dụ về việc triển khai, thay thế bằng logic dự đoán thực tế
            // Hiện tại, trả về một điểm số giả dựa trên temp và heart
            return (temp + heart) / 200.0;
        }
    }
}
