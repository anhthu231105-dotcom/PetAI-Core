using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories; //

namespace BUS.Services
{
    public class ServiceBUS
    {
        // Khởi tạo Repo để gọi xuống SQL
        private readonly ServiceRepository _serRepo = new ServiceRepository();

        // 1. Lấy toàn bộ danh sách dịch vụ cho DataGridView
        public DataTable GetAllServices()
        {
            // Gọi đúng tên hàm GetDataTable() trong Repository
            return _serRepo.GetDataTable();
        }
        // 2. Thêm dịch vụ (Khớp với các tham số id, tên, giá, ghi chú)
        public bool AddService(string id, string name, double price, string note)
        {
            // Có thể thêm logic: nếu giá < 0 thì return false không cho lưu
            if (price < 0) return false;
            return _serRepo.Insert(id, name, price, note);
        }

        // 3. Sửa dịch vụ
        public bool UpdateService(string id, string name, double price, string note)
        {
            return _serRepo.Insert(id, name, price, note); // Thư kiểm tra hàm Update ở DAL nhé
        }

        // 4. Xóa dịch vụ
        public bool DeleteService(string id)
        {
            return _serRepo.Delete(id);
        }
    }
}
