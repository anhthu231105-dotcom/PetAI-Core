using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Entities
{
    public class StayRegistrationDTO
    {
        public string StayID { get; set; }
        public string PetID { get; set; }
        public string EmployeeID { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime? ExpectedCheckOutDate { get; set; }
        public DateTime? ActualCheckOutDate { get; set; }
        public string StayReason { get; set; }
        public string DailyStatus { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalStayAmount { get; set; } // Chỉ đọc từ SQL
        public string Status { get; set; }
    }
}
