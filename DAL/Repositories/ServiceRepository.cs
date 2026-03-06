using DAL.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DAL.Core;

namespace DAL.Repositories
{
    public class ServiceRepository
    {
        // 1. Lấy toàn bộ danh sách dịch vụ
        public DataTable GetDataTable()
        {
            string query = "SELECT ServiceID, ServiceName, Price, Note FROM Services";
            // Dùng DataProvider.Instance vì constructor của Thư là private
            // Dùng ExecuteQuery vì đó là tên hàm Thư đặt trong DataProvider
            return DataProvider.Instance.ExecuteQuery(query);
        }

        // 2. Thêm mới một dịch vụ
        public bool Insert(string id, string name, double price, string note)
        {
            string query = string.Format(
                "INSERT INTO Services (ServiceID, ServiceName, Price, Note) VALUES ('{0}', N'{1}', {2}, N'{3}')",
                id, name, price, note);

            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        // 3. Cập nhật dịch vụ
        public bool Update(string id, string name, double price, string note)
        {
            string query = string.Format(
                "UPDATE Services SET ServiceName = N'{1}', Price = {2}, Note = N'{3}' WHERE ServiceID = '{0}'",
                id, name, price, note);

            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }

        // 4. Xóa dịch vụ
        public bool Delete(string id)
        {
            string query = string.Format("DELETE FROM Services WHERE ServiceID = '{0}'", id);
            return DataProvider.Instance.ExecuteNonQuery(query) > 0;
        }
    }
}
