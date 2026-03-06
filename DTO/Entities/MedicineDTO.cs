using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Entities
{
    public class MedicineDTO//Thuốc
    {
        public string MedicineID { get; set; }
        public string MedicineName { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
