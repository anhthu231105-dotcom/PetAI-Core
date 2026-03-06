using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Gộp địa chỉ thành một chuỗi duy nhất để hiển thị lên bảng
        public string FullAddress { get; set; } = string.Empty;
    }
}
