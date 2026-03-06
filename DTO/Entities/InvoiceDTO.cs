using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Entities
{
    public class InvoiceDTO//Hóa đơn
    {
        public string InvoiceID { get; set; }
        public string PetID { get; set; }
        public string EmployeeID { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
