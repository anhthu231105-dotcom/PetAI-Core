//using BUS.Core;
//using BUS.Security;
using BUS.Services;
using DAL.Repositories;
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
    public partial class FrmLogin : Form
    {
        // Khởi tạo nghiệp vụ đăng nhập từ tầng BUS
        private readonly UserService _accBus = new UserService();
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text.Trim();
            string pass = txtPass.Text.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu!");
                return;
            }

            // Gọi BUS để kiểm tra thay vì fix cứng "123"
            if (_accBus.Authenticate(user, pass)) // <-- FIXED: Use Authenticate method
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo");
                this.Hide(); // Ẩn form đăng nhập

                FrmMain f = new FrmMain();
                f.ShowDialog(); // Hiện form chính lên (Dùng ShowDialog để nó giữ quyền ưu tiên)

                this.Close(); // Đóng hẳn form đăng nhập sau khi thoát form chính
            }

            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!", "Lỗi");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            {
                Application.Exit(); // Thoát toàn bộ ứng dụng
            }
        }
    }
}
