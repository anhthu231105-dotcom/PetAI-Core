using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Entities
{
    public class ServiceDTO//Dịch vụ
    {
        public string ServiceID { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
    }
}
