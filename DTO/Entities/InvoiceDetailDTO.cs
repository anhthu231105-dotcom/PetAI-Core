using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Entities
{
    public class InvoiceDetailDTO//Chi tiết hóa đơn
    {
        public int DetailID { get; set; }
        public string InvoiceID { get; set; }
        public string ServiceID { get; set; }
        public string MedicineID { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
    }
}
