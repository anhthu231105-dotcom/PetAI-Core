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

        public bool AddPet(string id, string name, string species, string breed, int age)
        {
            // Thư có thể thêm logic kiểm tra dữ liệu ở đây
            return _petRepo.Insert(id, name, species, breed, age);
        }

        public bool UpdatePet(string id, string name, string species, string breed, int age)
        {
            return _petRepo.Update(id, name, species, breed, age);
        }

        public bool DeletePet(string id)
        {
            return _petRepo.Delete(id);
        }
    }
}
