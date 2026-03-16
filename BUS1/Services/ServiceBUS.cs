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
        private readonly ServiceRepository _serRepo = new ServiceRepository();

        public DataTable GetAllServices() => _serRepo.GetDataTable();

        public bool AddService(string id, string name, double price, string note)
        {
            return _serRepo.Insert(id, name, price, note);
        }

        public bool UpdateService(string id, string name, double price, string note)
        {
            return _serRepo.Update(id, name, price, note);
        }

        public bool DeleteService(string id)
        {
            return _serRepo.Delete(id);
        }
        public string GetNewServiceID()
        {
            DataTable dt = _serRepo.GetDataTable();
            int count = dt.Rows.Count + 1;
            return "S" + count.ToString("D3"); // Tạo mã dạng S001, S002...
        }
    }
}
