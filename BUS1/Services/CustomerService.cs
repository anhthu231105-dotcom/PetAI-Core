using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace BUS.Services
{
    public class CustomerService
    {
        //Giả sử Thư dùng Repository để gọi xuống SQL
        private readonly CustomerRepository _customerRepo = new CustomerRepository();

        // 1. Hàm lấy danh sách khách hàng
        public List<Customer> GetAllCustomers()
        {
            return _customerRepo.GetAll();
        }

        // 2. Hàm THÊM khách hàng (Thư nhớ thêm tham số address nhé)
        public bool AddCustomer(string id, string name, string phone, string address)
        {
            // Chỗ này gọi xuống Repo để Insert vào SQL
            return _customerRepo.Insert(new Customer
            {
                CustomerID = id,
                CustomerName = name,
                Phone = phone,
                Address = address
            });
        }

        // 3. Hàm SỬA khách hàng
        public bool UpdateCustomer(string id, string name, string phone, string address)
        {
            return _customerRepo.Update(new Customer
            {
                CustomerID = id,
                CustomerName = name,
                Phone = phone,
                Address = address
            });
        }

        // 4. Hàm XÓA khách hàng
        public bool DeleteCustomer(string id)
        {
            return _customerRepo.Delete(id);
        }
    }
}
