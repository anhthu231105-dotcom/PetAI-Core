using DAL.Repositories;
using DTO.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.Transactions
{
    public class HealthAIResultDTO
    {
        public string Status { get; set; } = string.Empty; // "Bình thường" hoặc "Nguy cơ"
        public string Advice { get; set; } = string.Empty; // Lời khuyên cho khách hàng
        public bool NeedsAttention { get; set; } // Flag để GUI hiển thị màu đỏ/xanh
    }
}
