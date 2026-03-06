using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Entities;    // Kết nối tới các thực thể DTO

namespace BUS.Management
{
    public class ProductService
    {
            private readonly ProductRepository _repo = new ProductRepository();

            public DataTable GetAll() => _repo.GetListForDisplay();
        }
    }
