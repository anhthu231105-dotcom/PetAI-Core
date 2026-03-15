using BUS.Management;
using BUS.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GUI.Management
{
    public partial class FrmCustomer : Form
    {
        private readonly CustomerBUS _customerService = new CustomerBUS();
        public FrmCustomer()
        {
            InitializeComponent();
        }
        private void FrmCustomer_Load(object sender, EventArgs e) 
        {
            LoadData(); // Hàm này Thư viết để lấy danh sách từ SQL và gán vào dgvCustomer
            btnLamMoi_Click(null, null);
        }
        private void LoadData()
        {
            // Lấy danh sách khách hàng và đổ vào DataGridView
          //  dgvCustomer.DataSource = _customerService.GetAllCustomers();
            try
            {
                // Gán danh sách từ Service vào bảng
                dgvCustomer.DataSource = _customerService.GetAllCustomers();

                // Tắt cột trống ở đầu bảng cho đẹp nè Thư
                dgvCustomer.RowHeadersVisible = false;

                // Tự động căn chỉnh độ rộng cột cho khít với chữ
                dgvCustomer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Không nạp được dữ liệu rồi Thư ơi: " + ex.Message);
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string id = txtCustomerID.Text.Trim();
                string name = txtCustomerName.Text.Trim();
                string phone = txtPhone.Text.Trim();
                string address = txtAdd.Text.Trim(); // Lấy địa chỉ
                //string email = txtEmail.Text.Trim();

                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Thư ơi, đừng để trống Mã hoặc Tên khách hàng nha!");
                    return;
                }

                if (_customerService.AddCustomer(id, name, phone, address))
                {
                    MessageBox.Show("Thêm khách hàng thành công!");
                    btnLamMoi_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi rồi Thư ơi: " + ex.Message);
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {// Lấy dữ liệu từ các ô nhập
            string id = txtCustomerID.Text.Trim();
            string name = txtCustomerName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string address = txtAdd.Text.Trim();

            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Thư ơi, hãy chọn một khách hàng dưới bảng để sửa nhé!");
                return;
            }

            // Gọi Service để cập nhật dữ liệu xuống SQL
            if (_customerService.UpdateCustomer(id, name, phone, address))
            {
                MessageBox.Show("Cập nhật thông tin khách hàng thành công!");
                LoadData(); // Nạp lại bảng để thấy thay đổi
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            // 1. Lấy mã khách hàng từ ô nhập
            string id = txtCustomerID.Text.Trim();

            // 2. Kiểm tra xem Thư đã chọn khách hàng chưa
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Thư cần chọn khách hàng muốn xóa ở bảng bên dưới trước nhé!");
                return;
            }

            // 3. Hỏi xác nhận để tránh Thư lỡ tay xóa nhầm
            DialogResult confirm = MessageBox.Show($"Thư có chắc chắn muốn xóa khách hàng có mã {id} không?",
                                                   "Xác nhận xóa",
                                                   MessageBoxButtons.YesNo,
                                                   MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    // 4. Gọi xuống Service để thực hiện xóa trong SQL
                    if (_customerService.DeleteCustomer(id))
                    {
                        MessageBox.Show("Đã xóa khách hàng thành công rồi nè Thư!");

                        // Gọi nút Làm mới để xóa trắng các ô nhập và nạp lại bảng
                        btnLamMoi_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Xóa không thành công, Thư kiểm tra lại mã khách hàng nhé.");
                    }
                }
                catch (Exception ex)
                {
                    // 5. Xử lý lỗi ràng buộc (lỗi Thư vừa gặp)
                    if (ex.Message.Contains("FK_Pet_CustomerID")) // Kiểm tra xem có phải do dính tới bảng Pet không
                    {
                        MessageBox.Show("Không thể xóa được Thư ơi! Khách hàng này vẫn còn thú cưng trong hệ thống.\n\n" +
                                        "Thư hãy qua Form Quản lý thú cưng để xóa hết Pet của họ trước khi xóa khách hàng này nhé!");
                    }
                    else
                    {
                        // Các lỗi khác (mất kết nối, lỗi SQL...)
                        MessageBox.Show("Có lỗi xảy ra rồi Thư ơi: " + ex.Message);
                    }
                }
            }
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {// Xóa trắng văn bản ở các ô
         // 1. Xóa trắng các ô nhập liệu khác
            txtCustomerName.Clear();
            txtPhone.Clear();
            txtAdd.Clear();

            // 2. Gọi hàm tự nhảy mã từ Service và gán vào ô ID
            txtCustomerID.Text = _customerService.GetNewCustomerID();

            // 3. Khóa ô ID lại để Thư không cần (và không được) sửa mã tự động
            txtCustomerID.ReadOnly = true;

            // 4. Load lại bảng dữ liệu
            LoadData();

            // 5. Để con trỏ vào ô Tên cho Thư nhập luôn cho lẹ
            txtCustomerName.Focus();
        
        }
        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {// Kiểm tra xem Thư có nhấn trúng dòng dữ liệu không (tránh nhấn vào tiêu đề)
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCustomer.Rows[e.RowIndex];

                // Lấy dữ liệu theo số thứ tự cột (0, 1, 2, 3) để tránh lỗi "không tìm thấy cột"
                txtCustomerID.Text = row.Cells[0].Value?.ToString();   // Cột Mã KH
                txtCustomerName.Text = row.Cells[1].Value?.ToString(); // Cột Tên KH
                txtPhone.Text = row.Cells[2].Value?.ToString();        // Cột Số ĐT
                txtAdd.Text = row.Cells[3].Value?.ToString();      // Cột Địa chỉ

                // Khóa ô Mã lại vì Mã là duy nhất, không nên cho sửa
                txtCustomerID.ReadOnly = true;
            }
        }
        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}
