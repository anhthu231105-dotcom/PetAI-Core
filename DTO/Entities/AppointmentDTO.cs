using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Entities
{
    public class AppointmentDTO//Lịch chăm sóc
    {
        public string AppointmentID { get; set; } // Thay cho MaDatLich
        public string PetID { get; set; }         // Thay cho MaThuCung
        public string CustomerID { get; set; }    // Mã khách hàng đặt lịch
        public DateTime AppointmentDate { get; set; }
        public string Description { get; set; }    // Nội dung hẹn (khám bệnh/spa)
        public string Status { get; set; }         // Trạng thái: Pending, Confirmed, Cancelled
    }
}