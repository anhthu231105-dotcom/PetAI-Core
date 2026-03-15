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
            public DataTable GetDataTable()
            {
                string query = "SELECT ServiceID, ServiceName, Price, Note FROM Service";
                return DataProvider.Instance.ExecuteQuery(query);
            }

            // 2. Thêm mới sử dụng tham số @
            public bool Insert(string id, string name, double price, string note)
            {
                string query = "INSERT INTO Service (ServiceID, ServiceName, Price, Note) VALUES ( @id , @name , @price , @note )";

                // Tạo mảng object chứa các giá trị theo đúng thứ tự @ trong query
                object[] parameter = new object[] { id, name, price, note };

                return DataProvider.Instance.ExecuteNonQuery(query, parameter) > 0;
            }

            // 3. Cập nhật sử dụng tham số @
            public bool Update(string id, string name, double price, string note)
            {
                string query = "UPDATE Service SET ServiceName = @name , Price = @price , Note = @note WHERE ServiceID = @id";

                object[] parameter = new object[] { name, price, note, id };

                return DataProvider.Instance.ExecuteNonQuery(query, parameter) > 0;
            }

            // 4. Xóa sử dụng tham số @
            public bool Delete(string id)
            {
                string query = "DELETE FROM Service WHERE ServiceID = @id";

                return DataProvider.Instance.ExecuteNonQuery(query, new object[] { id }) > 0;
            }
        }
    }
