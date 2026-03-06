using DAL.Core;
using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProductRepository
    {
        // 1. Lấy danh sách sản phẩm hiển thị lên DataGridView
        public DataTable GetListForDisplay()
        {
            string query = @"SELECT p.Id, p.Name, p.Price, p.Quantity, c.Name as CategoryName 
                            FROM Product p 
                            INNER JOIN Category c ON p.CategoryId = c.Id 
                            WHERE p.IsDeleted = 0";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        // 2. Thêm sản phẩm mới sử dụng ViewModel ProductRequest
        public bool Add(ProductRequest request)
        {
            string query = "INSERT INTO Product (Id, Name, Price, CategoryId, CreatedAt) " +
                           "VALUES ( @Id , @Name , @Price , @CategoryId , GETDATE() )";

            object[] parameter = { Guid.NewGuid(), request.Name, request.Price, request.CategoryId };

            int result = DataProvider.Instance.ExecuteNonQuery(query, parameter);
            return result > 0;
        }

        // 3. Xóa sản phẩm (Xóa mềm bằng cách cập nhật IsDeleted)
        public bool Delete(Guid id)
        {
            string query = "UPDATE Product SET IsDeleted = 1 WHERE Id = @Id";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { id });
            return result > 0;
        }
    }
}
