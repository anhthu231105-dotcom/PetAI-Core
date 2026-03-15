using GUI.Management;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.Security
{
    public partial class FrmMain : Form
    {
        private Form activeForm = null;
        public FrmMain()
        {
            InitializeComponent();
            this.IsMdiContainer = false; // Chúng ta dùng Panel thay vì MDI
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close(); // Đóng form hiện tại để giải phóng bộ nhớ
            }

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            pnlContent.Controls.Add(childForm);
            pnlContent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            lblTitle.Text = "HỆ THỐNG QUẢN LÝ PETAI CORE";
           //lblAdmin.Text = "Xin chào: " + (GlobalConfig.CurrentUser ?? "Admin");
        }

        private void btnPet_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "QUẢN LÝ THÚ CƯNG";
            // Đảm bảo bạn đã tạo file FrmPetManager.cs trong project GUI
            OpenChildForm(new FrmPetManager());
        }

        private void btnAI_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "CHẨN ĐOÁN SỨC KHỎE AI";
            // Đảm bảo bạn đã tạo file FrmAIPrediction.cs trong project GUI
            OpenChildForm(new FrmAIPrediction());
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "DỊCH VỤ KÝ GỬI";
             OpenChildForm(new FrmService());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Anh Thư có muốn đăng xuất không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                FrmLogin login = new FrmLogin();
                login.ShowDialog();
                this.Close();
            }
        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "LẬP HÓA ĐƠN THANH TOÁN";
            // Kết nối với bảng Invoice trong SQL của bạn
            OpenChildForm(new FrmInvoice());
        }

        /*private void btnAccount_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "THÔNG TIN TÀI KHOẢN";
            // Xem thông tin người đang đăng nhập
           //OpenChildForm(new FrmAccount());
        }*/

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            // Gọi hàm mở Form con và truyền FrmCustomer vào
            OpenChildForm(new FrmCustomer());

            // (Tùy chọn) Thư có thể đổi tiêu đề Form Main để biết mình đang ở trang nào
            this.Text = "PetAI Core - Quản lý Khách hàng";
        }
        private void btnAccount_Click(object sender, EventArgs e)
        {
            // Để trống để hết lỗi giao diện nè Thư
        }
    }
}
   
