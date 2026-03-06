using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        // Khóa ngoại liên kết tới Category
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; } = null;
    }
}
