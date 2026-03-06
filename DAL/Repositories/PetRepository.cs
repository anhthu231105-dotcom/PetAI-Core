using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    //Thư nhớ đổi chuỗi kết nối này cho khớp với tên Server của bạn nhé
    public class PetRepository 
    {
        private string connectionString = @"Data Source=DESKTOP-P9T2K3A\SQLEXPRESS;Initial Catalog=PetAI_Core_DB;Integrated Security=True";

        // 1. Lấy danh sách thú cưng
        public DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Pet"; // Đảm bảo tên bảng trong SQL là Pet
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }
            return dt;
        }

        // 2. Thêm thú cưng mới
        public bool Insert(string id, string name, string species, string breed, int age)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Pet (PetID, PetName, Species, Breed, Age) VALUES (@id, @name, @species, @breed, @age)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@species", species);
                cmd.Parameters.AddWithValue("@breed", breed);
                cmd.Parameters.AddWithValue("@age", age);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // 3. Cập nhật thông tin
        public bool Update(string id, string name, string species, string breed, int age)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Pet SET PetName=@name, Species=@species, Breed=@breed, Age=@age WHERE PetID=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@species", species);
                cmd.Parameters.AddWithValue("@breed", breed);
                cmd.Parameters.AddWithValue("@age", age);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // 4. Xóa thú cưng
        public bool Delete(string id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Pet WHERE PetID=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
