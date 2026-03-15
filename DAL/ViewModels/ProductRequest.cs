using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DAL.ViewModels
{
    public class ProductRequest
    {
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        public string Name { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        public Guid CategoryId { get; set; }
    }
}
