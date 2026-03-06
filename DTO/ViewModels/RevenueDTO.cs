using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModels
{
    public class RevenueDTO // Doanh thu theo kỳ (ngày, tháng)
    {
        // Thời gian báo cáo (có thể là ngày hoặc tháng)
        public string TimePeriod { get; set; }

        // Tổng số hóa đơn trong kỳ
        public int TotalInvoices { get; set; }

        // Tiền từ dịch vụ (Spa, khám bệnh...)
        public decimal ServiceRevenue { get; set; }

        // Tiền từ bán thuốc/vật tư
        public decimal MedicineRevenue { get; set; }

        // Tiền từ việc gửi thú cưng nội trú
        public decimal StayRevenue { get; set; }

        // Tổng cộng doanh thu cuối cùng
        public decimal TotalRevenue { get; set; }
    }
}

