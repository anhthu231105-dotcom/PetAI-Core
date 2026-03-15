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

    public partial class FrmInvoice : Form
    {
        // Khai báo các lớp nghiệp vụ (BUS)
        private ServiceBUS _serBus = new ServiceBUS();
        private CustomerBUS _cusBus = new CustomerBUS(); // Giả định Thư đã có CustomerBUS
        private double _totalAmount = 0;
        private readonly PetService _petBus = new PetService();
       // PetBUS _petBus = new PetBUS();

        public FrmInvoice()
        {
            InitializeComponent();
        }

        private void FrmInvoice_Load(object sender, EventArgs e)
        {
            LoadDataToCombobox();
            //SetupDataGridView();
            txtInvoiceID.Text = "HD" + DateTime.Now.ToString("yyyyMMddHHmm"); // Tự tạo mã HD tạm thời
        }
        // 1. ĐỔ DỮ LIỆU VÀO COMBOBOX
        // CHỈ GIỮ LẠI 1 HÀM NÀY DUY NHẤT (Đã sửa lỗi trùng)
        private void LoadDataToCombobox()
        {
            try
            {
                // 1. Đổ danh sách Dịch vụ
                cbService.DataSource = _serBus.GetAllServices();
                cbService.DisplayMember = "ServiceName";
                cbService.ValueMember = "ServiceID";

                // 2. Đổ danh sách Khách hàng 
                // LƯU Ý: Nếu lỗi GetAllCustomers, Thư hãy thử đổi tên thành GetAll() hoặc GetDataTable()
                cbCustomer.DataSource = _cusBus.GetAllCustomers(); // Hoặc .GetAll()
                cbCustomer.DisplayMember = "CustomerName";
                cbCustomer.ValueMember = "CustomerID";

                cbService.SelectedIndex = -1;
                cbCustomer.SelectedIndex = -1;
            }
            catch (Exception)
            {
                // Xóa bỏ catch trống để dễ tìm lỗi nếu có
                // MessageBox.Show("Lỗi load dữ liệu: " + ex.Message);
            }
        }

        private void cbService_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbService.SelectedItem != null && cbService.SelectedIndex != -1)
            {
                // Lấy dòng dữ liệu đang được chọn trong ComboBox
                DataRowView row = cbService.SelectedItem as DataRowView;
                if (row != null)
                {
                    // Hiển thị giá tiền vào ô txtPrice (Thư nhớ kiểm tra tên textbox đơn giá nhé)
                    txtPrice.Text = row["Price"].ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cbService.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn một dịch vụ!");
                return;
            }

            // Lấy thông tin từ các ô nhập liệu
            string serviceName = cbService.Text;
            string priceStr = txtPrice.Text;
            //string note = txtNote.Text; // Ô ghi chú nếu Thư có vẽ

            if (double.TryParse(priceStr, out double price))
            {
                // Thêm một dòng mới vào DataGridView (dgvBill)
                // Thứ tự cột: STT, Tên dịch vụ, Đơn giá, Ghi chú
                int stt = dgvBill.Rows.Count + 1;
                dgvBill.Rows.Add(stt, serviceName, price); //note);

                // Cập nhật tổng tiền thanh toán
                _totalAmount += price;
                label7.Text = _totalAmount.ToString("N0") + " VNĐ"; // Định dạng số cho đẹp

                // Xóa trắng ô chọn dịch vụ để chọn cái mới
                cbService.SelectedIndex = -1;
                txtPrice.Clear();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtInvoiceID.Text = "HD" + DateTime.Now.ToString("yyyyMMddHHmm");
            cbCustomer.SelectedIndex = -1;
            cbService.SelectedIndex = -1;
            txtPrice.Clear();
            dgvBill.Rows.Clear();
            _totalAmount = 0;
            label7.Text = "0 VNĐ";
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThanhToan_IN_Click(object sender, EventArgs e)
        {
            if (dgvBill.Rows.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống nha Thư!");
                return;
            }

            // 1. Gọi Form xác nhận thanh toán (Chỉ gửi qua đúng số tiền tổng thôi)
            using (FrmPaymentConfirm frmPay = new FrmPaymentConfirm(_totalAmount))
            {
                // 2. Nếu khách bấm "Xác nhận" bên Form đó
                if (frmPay.ShowDialog() == DialogResult.OK)
                {
                    // 3. Ở đây Thư có thể viết code Lưu vào Database (nếu muốn)
                    // SaveInvoiceToDatabase(); 

                    MessageBox.Show("Đã hoàn tất thanh toán!", "Thành công");

                    // 4. Làm mới Form để đón khách tiếp theo
                    btnReset_Click(sender, e);
                }
            }
        }
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            // Thư cứ để trống ở đây, lát mình viết code thanh toán sau nhé!
            // 1. Kiểm tra xem Thư đã thêm dịch vụ vào bảng chưa
            if (dgvBill.Rows.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống nha Thư!");
                return;
            }

            // 2. Gọi Form xác nhận thanh toán (FrmPaymentConfirm)
            // Thư nhớ kiểm tra xem tên class bên kia có đúng là FrmPaymentConfirm không nhé
            using (FrmPaymentConfirm frmPay = new FrmPaymentConfirm(_totalAmount))
            {
                // 3. Nếu bấm xác nhận bên Form kia thì mới hiện thông báo thành công
                if (frmPay.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Đã hoàn tất thanh toán cho bé " + cbPet.Text + "!", "Thành công");

                    // Làm mới Form để đón khách tiếp theo
                    btnReset_Click(sender, e);
                }
            }
        }
        private void LoadPetByCustomer(string customerID)
        {
            DataTable dtPet = _petBus.GetPetsByCustomerID(customerID);
            cbPet.DataSource = dtPet;
            cbPet.DisplayMember = "PetName"; // Hiện tên thú cưng (Lu Lu, Bống...)
            cbPet.ValueMember = "PetID";     // Giá trị ẩn bên dưới là mã

            if (dtPet.Rows.Count == 0)
            {
                cbPet.Text = "Khách này chưa có thú cưng";
            }
        }

        private void cbPet_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem ComboBox có đang chọn một khách hàng hợp lệ không
            if (cbCustomer.SelectedValue != null && cbCustomer.SelectedIndex != -1)
            {
                try
                {
                    // 2. Lấy CustomerID từ giá trị được chọn (ValueMember đã cài ở LoadData)
                    string maKH = cbCustomer.SelectedValue.ToString();

                    // 3. Gọi hàm tải danh sách thú cưng dựa trên mã khách hàng này
                    LoadPetByCustomer(maKH);
                }
                catch (Exception )
                {
                    // Thư có thể hiện lỗi nếu muốn, hoặc để trống để không làm phiền người dùng
                    // MessageBox.Show("Lỗi khi lọc thú cưng: " + ex.Message);
                }
            }
            else
            {
                // Nếu không chọn khách nào thì xóa trắng ô Thú cưng
                cbPet.DataSource = null;
            }
        }
    }
}
