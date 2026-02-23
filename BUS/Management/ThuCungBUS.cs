using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories; // Kết nối tới tầng DAL
using DTO.Entities;    // Kết nối tới các thực thể DTO

namespace BUS.Management
{
    public class ThuCungBUS
    {
        // Khởi tạo đối tượng DAL để giao tiếp với Database
        private ThuCungDAL dal = new ThuCungDAL();

        /// <summary>
        /// Lấy toàn bộ danh sách thú cưng để hiển thị lên GridView
        /// </summary>
        public DataTable LayDanhSach()
        {
            return dal.GetAll();
        }

        /// <summary>
        /// Xử lý logic Thêm hoặc Cập nhật thông tin thú cưng
        /// </summary>
        /// <param name="pet">Đối tượng thú cưng từ giao diện</param>
        /// <param name="isUpdate">True nếu là cập nhật, False nếu là thêm mới</param>
        public bool LuuThuCung(ThuCungDTO pet, bool isUpdate)
        {
            // 1. Kiểm tra logic nghiệp vụ cơ bản
            if (string.IsNullOrEmpty(pet.TenThuCung))
            {
                return false; // Tên không được để trống
            }

            if (pet.CanNang <= 0)
            {
                return false; // Cân nặng phải lớn hơn 0
            }

            // 2. Gọi xuống tầng DAL để thực thi SQL
            if (isUpdate)
            {
                // Cập nhật thông tin bé pet đã có
                return dal.Update(pet.MaThuCung, pet.TenThuCung, pet.CanNang ?? 0);
            }
            else
            {
                // Thêm mới bé pet vào hệ thống
                return dal.Insert(
                    pet.MaThuCung,
                    pet.TenThuCung,
                    pet.Loai,
                    pet.Giong,
                    pet.Tuoi ?? 0,
                    pet.CanNang ?? 0,
                    pet.MaKhachHang
                );
            }
        }

        /// <summary>
        /// Xóa thú cưng theo mã
        /// </summary>
        public bool XoaThuCung(string maPet)
        {
            if (string.IsNullOrEmpty(maPet)) return false;
            // Lưu ý: Bạn cần bổ sung hàm Delete trong ThuCungDAL nếu chưa có
            // return dal.Delete(maPet) > 0;
            return true;
        }
    }
}
