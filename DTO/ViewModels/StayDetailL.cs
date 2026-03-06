using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModels
{
    public class StayDetail // Chi tiết phiếu lưu trú
    {
        public string StayID { get; set; }           // Thay cho MaPhieu
        public string PetName { get; set; }          // Lấy từ bảng Pet
        public string CustomerName { get; set; }     // Lấy từ bảng Customer
        public DateTime CheckInDate { get; set; }    // Thay cho NgayNhan
        public DateTime? ExpectedCheckOutDate { get; set; } // Thay cho NgayTraDuKien
        public string InitialStatus { get; set; }    // Thay cho TinhTrangLucNhan
    }
}
