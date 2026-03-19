using DAL.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class PetRepository
    {
        public DataTable GetDataTable()
        {
            string query = "SELECT * FROM Pet";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        // 1. Thêm mới Pet: Đã thêm Weight vào SQL và khớp tham số
        public bool Insert(string id, string name, string species, string breed, int age, double weight, string customerID)
        {
            string query = "INSERT INTO Pet (PetID, PetName, Species, Breed, Age, Weight, CustomerID) " +
               "VALUES ( @id , @name , @species , @breed , @age , @weight , @customerID )";

            object[] parameter = new object[] { id, name, species, breed, age, weight, customerID };

            return DataProvider.Instance.ExecuteNonQuery(query, parameter) > 0;
        }

        // 2. Cập nhật Pet: Đã thêm cập nhật Weight và CustomerID
        public bool Update(string id, string name, string species, string breed, int age, double weight, string customerID)
        {
            string query = "UPDATE Pet SET PetName = @name , Species = @species , Breed = @breed , Age = @age , Weight = @weight , CustomerID = @customerID WHERE PetID = @id";

            // Thứ tự này đã chuẩn rồi: name, species, breed, age, weight, customerID, id
            object[] parameter = new object[] { name, species, breed, age, weight, customerID, id };

            return DataProvider.Instance.ExecuteNonQuery(query, parameter) > 0;
        }

        // 3. Xóa Pet
        public bool Delete(string id)
        {
            string query = "DELETE FROM Pet WHERE PetID = @id";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { id }) > 0;
        }
        public DataTable GetPetsByCustomer(string customerID)
        {
            // Lệnh SQL để lấy đúng thú cưng của khách hàng đang chọn
            string query = "SELECT PetID, PetName FROM Pet WHERE CustomerID = @id";
            object[] parameter = new object[] { customerID };

            // Gọi DataProvider để thực thi câu lệnh
            return DataProvider.Instance.ExecuteQuery(query, parameter);
        }
        public string GetMaxPetID()
        {
            // Lệnh này lấy ra ID lớn nhất trong bảng Pet
            string query = "SELECT MAX(PetID) FROM Pet";
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);

            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
            {
                return dt.Rows[0][0].ToString();
            }
            return null;
        }
    }
}
