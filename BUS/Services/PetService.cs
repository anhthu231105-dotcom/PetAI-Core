using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories;

namespace BUS.Services
{
    public class PetService
    {
        private readonly PetRepository _petRepo = new PetRepository();

        public DataTable GetAllPets() => _petRepo.GetDataTable();

        // Thêm string customerID vào tham số đầu vào
        public bool AddPet(string id, string name, string species, string breed, int age, double weight, string customerID)
        {
            // Truyền đủ 7 tham số xuống DAL theo đúng thứ tự
            return _petRepo.Insert(id, name, species, breed, age, weight, customerID);
        }

        // Thêm string customerID vào tham số đầu vào
        public bool UpdatePet(string id, string name, string species, string breed, int age, double weight, string customerID)
        {
            // Truyền đủ 7 tham số xuống DAL theo đúng thứ tự
            return _petRepo.Update(id, name, species, breed, age, weight, customerID);
        }

        public bool DeletePet(string id)
        {
            return _petRepo.Delete(id);
        }
        public string GetNewPetID()
        {
            // Giả sử Thư lấy danh sách về rồi đếm số dòng
            int count = _petRepo.GetDataTable().Rows.Count + 1;
            return "P" + count.ToString("D3"); // Kết quả: P001, P002...
        }
        public DataTable GetPetsByCustomerID(string customerID)
        {
            // Hàm này sẽ gọi xuống Repository để lọc Pet theo chủ
            return _petRepo.GetPetsByCustomer(customerID);
        }
    }   
}
