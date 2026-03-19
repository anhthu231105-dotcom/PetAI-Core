using BUS.Management;
using BUS.Services;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using BUS.Services;


namespace GUI.Management
{
    public partial class FrmPetManager : Form
    {
        private readonly PetService _petService = new PetService();

        public FrmPetManager()
        {
            InitializeComponent();
        }

        private void FrmPetManager_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                // 1. Xử lý cho DataGridView (Danh sách thú cưng)
                dgvPet.AutoGenerateColumns = false; // Tắt tự tạo cột nếu Thư đã thiết kế cột bằng tay
                DataTable dtPet = _petService.GetAllPets();
                dgvPet.DataSource = dtPet;

                CustomerBUS _cusService = new CustomerBUS();

                // SỬA TẠI ĐÂY: Dùng var hoặc List<Customer> thay cho DataTable
                var dsCustomer = _cusService.GetAllCustomers();

                if (dsCustomer != null && dsCustomer.Count > 0)
                {
                    cboCustomer.DataSource = null;
                    cboCustomer.DataSource = dsCustomer; // Gán trực tiếp List vào nguồn dữ liệu

                    // Đảm bảo tên thuộc tính khớp chính xác với class Customer
                    cboCustomer.DisplayMember = "CustomerName";
                    cboCustomer.ValueMember = "CustomerID";

                    cboCustomer.SelectedIndex = -1; // Để trống lúc mới hiện ra cho Thư dễ chọn
                    txtCustomerID.Clear();
                    txtPetID.Text = _petService.GetNewPetID();
                    txtPetID.ReadOnly = true; // Khóa lại không cho sửa mã này
                    dgvPet.DataSource = _petService.GetAllPets();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
            
            // Ví dụ logic tạo mã tự động đơn giản
            private void GeneratePetID()
        {
            int count = _petService.GetAllPets().Rows.Count + 1;
            txtPetID.Text = "P" + count.ToString("D3"); // Kết quả ra P001, P002...
        
        }

        private void dgvPet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvPet_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPet.Rows[e.RowIndex];

                // Gán dữ liệu từ cột vào đúng tên các nút của Thư
                txtPetID.Text = row.Cells["colPetID"].Value.ToString();
                txtPetName.Text = row.Cells["colPetName"].Value.ToString();
                cboSpecies.Text = row.Cells["colSpecies"].Value.ToString();
                txtBreed.Text = row.Cells["colBreed"].Value.ToString();
                txtAge.Text = row.Cells["colAge"].Value.ToString();
                // THÊM DÒNG NÀY ĐỂ HIỆN CÂN NẶNG
                txtWeight.Text = row.Cells["colWeight"].Value.ToString();

                txtPetID.ReadOnly = true; // Khóa mã lại khi đang sửa
            }
        }

        private void txtPetName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra dữ liệu trống
                if (string.IsNullOrEmpty(txtPetID.Text) || string.IsNullOrEmpty(txtPetName.Text))
                {
                    MessageBox.Show("Vui lòng nhập Mã và Tên thú cưng!");
                    return;
                }

                // Lấy dữ liệu từ tên các nút bạn đã đặt
                string id = txtPetID.Text;
                string name = txtPetName.Text;
                string species = cboSpecies.Text;
                string breed = txtBreed.Text;
                int age = int.Parse(txtAge.Text);
                double weight = double.Parse(txtWeight.Text); // Lấy cân nặng từ TextBox
                                                              // string customerID = cboCustomer.SelectedValue.`ToString(); // Lấy
                                                              // Thay vì chỉ kiểm tra SelectedValue == null
                                                              // 1. Kiểm tra an toàn trước khi lấy mã khách hàng
                if (string.IsNullOrEmpty(txtCustomerID.Text))
                {
                    MessageBox.Show("Vui lòng chọn khách hàng!");
                    return;
                }

                // 2. Lấy mã trực tiếp từ TextBox (vì nó đã nhảy mã KH001 rồi)
                // Đừng lấy từ cboCustomer.SelectedValue nữa để tránh lỗi Null
                string customerID = txtCustomerID.Text;

                // 3. Gọi hàm thêm thú cưng
                if (_petService.AddPet(id, name, species, breed, age, weight, customerID))
                {
                    MessageBox.Show("Thêm thành công!");
                    LoadData(); // Nạp lại bảng ngay sau khi thêm
                    btnReset_Click(null, null);
                }
                // 2. Lấy giá trị (Bỏ dấu chú thích đi nhé Thư)
                //string customerID = cboCustomer.SelectedValue.ToString();
                /* if (_petService.AddPet(id, name, species, breed, age, weight, customerID))
                 {
                     MessageBox.Show("Thêm thành công!");
                     LoadData(); // Thông tin nhảy xuống bảng ngay lập tức
                     btnReset_Click(null, null); // Làm mới ô nhập
                 }*/

                /* if (cboCustomer.SelectedValue == null)
                 {
                     MessageBox.Show("Vui lòng chọn chủ nuôi (Khách hàng) cho bé!");
                     return;
                 }
                 string customerID = cboCustomer.SelectedValue.ToString();*/
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi định dạng: Tuổi phải là số!");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Kiểm tra xem Thư đã chọn dòng nào dưới bảng chưa
                if (string.IsNullOrEmpty(txtPetID.Text))
                {
                    MessageBox.Show("Thư ơi, hãy chọn một bé thú cưng trong danh sách để sửa nhé!");
                    return;
                }

                // 2. Kiểm tra an toàn ComboBox Khách hàng để tránh lỗi "văng" chương trình
                if (cboCustomer.SelectedValue == null || cboCustomer.SelectedValue is DataRowView)
                {
                    MessageBox.Show("Thư chưa chọn chủ nuôi (Khách hàng). Hãy chọn lại tên khách hàng nhé!");
                    return;
                }

                // 3. Lấy dữ liệu từ các ô nhập liệu
                string id = txtPetID.Text;
                string name = txtPetName.Text;
                string species = cboSpecies.Text;
                string breed = txtBreed.Text;

                // Dùng TryParse để máy không bị lỗi nếu Thư lỡ nhập chữ vào ô số
                if (!int.TryParse(txtAge.Text, out int age)) age = 0;
                if (!double.TryParse(txtWeight.Text, out double weight)) weight = 0;

                // Lấy mã khách hàng an toàn
                string customerID = cboCustomer.SelectedValue.ToString();

                // 4. Gọi tầng BUS để cập nhật vào SQL
                bool result = _petService.UpdatePet(id, name, species, breed, age, weight, customerID);

                if (result)
                {
                    MessageBox.Show($"Đã cập nhật thông tin bé {name} thành công!");
                    LoadData(); // Cập nhật lại bảng ngay lập tức
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại. Thư kiểm tra lại kết nối SQL nhé!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống khi sửa: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem người dùng đã chọn dòng nào trên bảng chưa
            if (dgvPet.SelectedRows.Count == 0 && string.IsNullOrEmpty(txtPetID.Text))
            {
                MessageBox.Show("Vui lòng chọn một dòng trên bảng để xóa!", "Thông báo");
                return;
            }

            // 2. Lấy tên bé thú cưng để hiện thông báo xác nhận cho thân thiện
            string petName = txtPetName.Text;
            string petID = txtPetID.Text;

            // 3. Hỏi xác nhận (Tránh việc lỡ tay bấm nhầm)
            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa bé '{petName}' (Mã: {petID}) không?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    // 4. Gọi tầng BUS để thực hiện xóa trong Database SQL
                    bool isDeleted = _petService.DeletePet(petID);

                    if (isDeleted)
                    {
                        MessageBox.Show("Đã xóa thú cưng thành công!", "Thông báo");

                        // 5. Cập nhật lại DataGridView ngay lập tức
                        LoadData();

                        // 6. Xóa trắng các TextBox để sẵn sàng cho thao tác tiếp theo
                        btnReset_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại. Vui lòng kiểm tra lại!", "Lỗi");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống: " + ex.Message);
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Xóa sạch nội dung trong các TextBox
            txtPetID.Clear();
            txtPetName.Clear();
            txtBreed.Clear();
            txtAge.Clear();
            // 2. BỔ SUNG: Xóa ô Cân nặng và ô Mã khách hàng
            txtWeight.Clear();
            txtCustomerID.Clear();

            // Đưa ComboBox về trạng thái chưa chọn
            cboSpecies.SelectedIndex = -1;

            // Mở khóa ô txtPetID (phòng trường hợp đang bị ReadOnly sau khi nhấn vào bảng)
            txtPetID.ReadOnly = false;

            // Đưa con trỏ chuột về ô đầu tiên để sẵn sàng nhập mới
            txtPetID.Focus();

            // Tải lại bảng để đảm bảo dữ liệu hiển thị mới nhất
            LoadData();
            //GeneratePetID();
        }

        private void txtAge_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra cực kỳ cẩn thận trước khi dùng
            if (cboCustomer.DataSource != null && cboCustomer.SelectedValue != null)
            {
                // Chỉ gán khi giá trị không phải là chính cái Row của bảng
                if (!(cboCustomer.SelectedValue is DataRowView))
                {
                    txtCustomerID.Text = cboCustomer.SelectedValue.ToString();
                }
            }
        }

        } }



