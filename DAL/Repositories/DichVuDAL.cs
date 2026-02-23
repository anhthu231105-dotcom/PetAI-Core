using DAL.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class DichVuDAL
    {
        DataProvider db = new DataProvider();

        public DataTable GetAll() => db.ExecuteQuery("SELECT * FROM DichVu");

        public DataTable GetByID(string maDV)
        {
            return db.ExecuteQuery("SELECT * FROM DichVu WHERE MaDichVu = @ma", new object[] { maDV });
        }
    }
}
