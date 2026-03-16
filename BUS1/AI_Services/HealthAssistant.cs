//using BUS.Core;
using BUS.Transactions;
using DAL.AI_Models; // Để gọi con AI từ tầng dưới
//using DAL.AI_Models;
using DAL.Repositories;
using DTO.AI_Models;
using DTO.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text; // <-- Add this import
// using BUS.DTOs;      // Để đóng gói kết quả trả về
//using Newtonsoft.Json;

namespace BUS.AI_Services
{

    public class HealthAssistant
    {
        // Chỉ giữ lại cái này để làm việc với AI
        private readonly HealthPredictionDAL _aiDAL = HealthPredictionDAL.Instance;

        public string GetAdvice(double temp, int heart)
        {
            try
            {
                var score = _aiDAL.Predict(temp, heart);

                if (score > 0.7)
                    return "⚠️ Cảnh báo: Thú cưng có dấu hiệu mệt mỏi, cần theo dõi thêm.";

                return "✅ Sức khỏe bé ổn định. Chúc bạn một ngày vui vẻ!";
            }
            catch (Exception ex)
            {
                return "❌ Lỗi hệ thống AI: " + ex.Message;
            }
        }
        public async Task<DTO.AI_Models.HealthPredictionDTO> GetAIHealthPrediction(string temp, string appetite, string activity)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var inputData = new { temp = float.Parse(temp), appetite = appetite, activity = activity };
                    var json = JsonConvert.SerializeObject(inputData);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("http://127.0.0.1:5000/predict", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        // Ép kiểu chuẩn để không bị lỗi "Unable to cast"
                        return JsonConvert.DeserializeObject<DTO.AI_Models.HealthPredictionDTO>(responseString);
                    }
                    throw new Exception("Server Python không phản hồi!");
                }
                catch (Exception ex)
                {
                    return new DTO.AI_Models.HealthPredictionDTO { Result = "Lỗi kết nối", Advice = ex.Message };
                }
            }
        }
    }

}