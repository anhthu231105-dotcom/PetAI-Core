using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Responses
{
    public class ServiceResponse
    {
        public bool Success { get; set; }  // True nếu thành công
        public string Message { get; set; } // Thông báo lỗi hoặc thành công
        public object Data { get; set; }    // Dữ liệu đi kèm nếu có
    }
}
