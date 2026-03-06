using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Entities
{
    public class ImportOrderDTO
    {
        // Mã phiếu nhập (VD: PN001, PN002)
        public string ImportID { get; set; }

        // Liên kết tới nhà cung cấp (SupplierID)
        public string SupplierID { get; set; }

        // Liên kết tới nhân viên thực hiện nhập hàng (EmployeeID)
        public string EmployeeID { get; set; }

        // Ngày nhập hàng
        public DateTime ImportDate { get; set; }

        // Tổng giá trị của phiếu nhập
        public decimal TotalAmount { get; set; }

        // Ghi chú thêm nếu cần (VD: Hàng dễ vỡ, thanh toán sau...)
        public string Note { get; set; }
    }
}
