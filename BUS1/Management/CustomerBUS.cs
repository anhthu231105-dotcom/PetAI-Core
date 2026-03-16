using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories; // Nhớ thêm using này để gọi được CustomerRepository nhé Thư
using DAL.Entities; // Nếu Thư có dùng class Customer ở DAL thì thêm using này nhé
namespace BUS.Management
{
    // 1. Phải có chữ PUBLIC ở đây để FrmInvoice truy cập được
    public class CustomerBUS
    {
        private CustomerRepository _cusRepo = new CustomerRepository();

        // 1. Lấy toàn bộ danh sách khách hàng
        public List<Customer> GetAllCustomers()
        {
            try { return _cusRepo.GetAll(); }
            catch { return null; }
        }

        // 2. Thêm khách hàng mới
        public bool AddCustomer(string id, string name, string phone, string address)
        {
            Customer c = new Customer { CustomerID = id, CustomerName = name, Phone = phone, Address = address };
            return _cusRepo.Insert(c);
        }

        // 3. Cập nhật thông tin khách hàng
        public bool UpdateCustomer(string id, string name, string phone, string address)
        {
            Customer c = new Customer { CustomerID = id, CustomerName = name, Phone = phone, Address = address };
            return _cusRepo.Update(c);
        }

        // 4. Xóa khách hàng
        public bool DeleteCustomer(string id)
        {
            return _cusRepo.Delete(id);
        }

        // 5. Lấy thú cưng theo mã khách hàng (Dùng cho Form Pet)
        public DataTable GetPetsByCustomerID(string customerID)
        {
            try { return _cusRepo.GetPetsByCustomer(customerID); }
            catch { return null; }
        }
        public string GetNewCustomerID()
        {
            try
            {
                var list = _cusRepo.GetAll(); // Lấy danh sách hiện tại
                if (list == null || list.Count == 0) return "KH001"; // Nếu chưa có ai thì bắt đầu từ 001

                // Lấy mã của người cuối cùng, ví dụ "KH005"
                string lastID = list.OrderByDescending(c => c.CustomerID).FirstOrDefault().CustomerID;

                // Tách phần số ra (bỏ chữ "KH"), tăng lên 1 và định dạng lại thành 3 chữ số
                int lastNum = int.Parse(lastID.Replace("KH", ""));
                return "KH" + (lastNum + 1).ToString("D3");
            }
            catch { return "KH001"; }
        }
    }
}