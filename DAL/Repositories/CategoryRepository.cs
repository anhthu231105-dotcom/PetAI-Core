using DAL.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CategoryRepository
    {
        // Lấy tất cả danh mục để hiện lên UI
        public DataTable GetAll()
        {
            string query = "SELECT Id, Name, Description FROM Category WHERE IsDeleted = 0";
            return DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
