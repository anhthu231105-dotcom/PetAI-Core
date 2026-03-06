using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Entities
{
    public class PetDTO//Thú cưng
    {
            public string PetID { get; set; }        // Khớp với VARCHAR(20)
            public string PetName { get; set; }      // Khớp với NVARCHAR(100)
            public string Species { get; set; }      // Loài (Chó, Mèo...)
            public string Breed { get; set; }        // Giống
            public string CustomerID { get; set; }   // Mã chủ nuôi
            public double Weight { get; set; }       // Cân nặng
        }
    }
