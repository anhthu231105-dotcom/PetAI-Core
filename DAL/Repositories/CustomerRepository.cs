using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Repositories
{
    public class CustomerRepository
    {
        // Thư thay chuỗi kết nối bằng chuỗi của máy Thư nhé
        private string connectionString = @"Data Source=DESKTOP-P9T2K3A\SQLEXPRESS;Initial Catalog=PetAI_Core_DB;Integrated Security=True";

        // 1. Hàm GetAll
        public List<Customer> GetAll()
        {
            List<Customer> list = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "SELECT CustomerID, CustomerName, Phone, Address FROM Customer";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(new Customer
                    {
                        CustomerID = dr["CustomerID"].ToString(),
                        CustomerName = dr["CustomerName"].ToString(),
                        Phone = dr["Phone"].ToString(),
                        Address = dr["Address"].ToString()
                    });
                }
            }
            return list;
        }

        // 2. Hàm Insert
        public bool Insert(Customer c)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO Customer(CustomerID, CustomerName, Phone, Address) VALUES(@id, @name, @phone, @add)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", c.CustomerID);
                cmd.Parameters.AddWithValue("@name", c.CustomerName);
                cmd.Parameters.AddWithValue("@phone", c.Phone);
                cmd.Parameters.AddWithValue("@add", c.Address);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // 3. Hàm Update
        public bool Update(Customer c)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "UPDATE Customer SET CustomerName=@name, Phone=@phone, Address=@add WHERE CustomerID=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", c.CustomerID);
                cmd.Parameters.AddWithValue("@name", c.CustomerName);
                cmd.Parameters.AddWithValue("@phone", c.Phone);
                cmd.Parameters.AddWithValue("@add", c.Address);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // 4. Hàm Delete
        public bool Delete(string id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "DELETE FROM Customer WHERE CustomerID=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        // Hàm này giúp lọc danh sách thú cưng theo mã khách hàng
        public DataTable GetPetsByCustomer(string customerID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Thư nhớ kiểm tra tên bảng và cột trong SQL của Thư nhé
                string sql = "SELECT * FROM Pet WHERE CustomerID = @cusID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@cusID", customerID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                conn.Open();
                da.Fill(dt);
            }
            return dt;
        }
    }
}
