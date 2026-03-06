using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        // Thay vì hiện CategoryId, ta hiện tên Category cho người dùng dễ đọc
        public string CategoryName { get; set; } = string.Empty;

        // Trạng thái tồn kho để hiển thị màu sắc trên UI (ví dụ: Hết hàng thì hiện chữ đỏ)
        public string StockStatus => Quantity > 0 ? "Còn hàng" : "Hết hàng";
    }
}
