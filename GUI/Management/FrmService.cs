using BUS.Management; // Thư kiểm tra namespace của file ServiceBUS nhé
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS.Services;

namespace GUI.Management
{
    public partial class FrmService : Form
    {
        private ServiceBUS _serBus = new ServiceBUS();
        public FrmService()
        {
            InitializeComponent();
        }

        private void FrmService_Load(object sender, EventArgs e)
        {
            LoadData();
          
        }
        private void LoadData()
        {
            dgvService.DataSource = _serBus.GetAllServices();
        }
        private void SetupGrid()
        {
            dgvService.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvService.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            // Định dạng tiền tệ cho cột Đơn giá (cột số 2 hoặc tên là Price)
            dgvService.Columns["Price"].DefaultCellStyle.Format = "#";
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string id = txtServiceID.Text;
                string name = txtServiceName.Text;
                double price = double.Parse(txtPrice.Text);
                string note = txtNote.Text;

                // 3. Gọi đúng hàm AddService (4 tham số), máy sẽ không đòi 'age' nữa
                if (_serBus.AddService(id, name, price, note))
                {
                    MessageBox.Show("Thêm dịch vụ mới thành công!");
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại, kiểm tra lại mã trùng không nhé!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi rồi bạn ơi: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string id = txtServiceID.Text;
                string name = txtServiceName.Text;
                double price = double.Parse(txtPrice.Text);
                string note = txtNote.Text;

                if (_serBus.UpdateService(id, name, price, note))
                {
                    MessageBox.Show("Cập nhật dịch vụ thành công!");
                    LoadData(); // Load lại bảng cho mới
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Thư có chắc muốn xóa dịch vụ này không?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string id = txtServiceID.Text;
                if (_serBus.DeleteService(id))
                {
                    MessageBox.Show("Đã xóa dịch vụ!");
                    btnLamMoi_Click(sender, e); // Xóa xong thì xóa trắng ô nhập luôn
                    LoadData();
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtServiceID.Clear();
            txtServiceName.Clear();
            txtPrice.Clear();
            txtNote.Clear();

            txtServiceID.ReadOnly = false; // Cho phép nhập lại mã nếu đang khóa
            txtServiceID.Focus(); // Đưa con trỏ chuột về ô Mã dịch vụ
        }

        private void dgvService_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvService.Rows[e.RowIndex];
                txtServiceID.Text = row.Cells["ServiceID"].Value.ToString();
                txtServiceName.Text = row.Cells["ServiceName"].Value.ToString();
                txtPrice.Text = row.Cells["Price"].Value.ToString();
                txtNote.Text = row.Cells["Note"].Value.ToString();

                txtServiceID.ReadOnly = true; // Khóa mã dịch vụ lại không cho sửa ID
            }
        }
    }
}

