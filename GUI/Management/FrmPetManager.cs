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
            dgvPet.DataSource = _petService.GetAllPets();
            // Nếu Thư đã tạo cột sẵn trong Designer, nhớ tắt AutoGenerateColumns = false;
            // Lấy dữ liệu mới nhất từ database
            DataTable dt = _petService.GetAllPets();

            // Gán vào bảng
            dgvPet.DataSource = dt;

            // Nếu Thư đã tạo cột tay trong Designer, nhớ dòng này:
            dgvPet.AutoGenerateColumns = false;
           // dgvPet.AutoGenerateColumns = false;
            dgvPet.DataSource = _petService.GetAllPets();
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
                txtPetID.Text = row.Cells["PetID"].Value.ToString();
                txtPetName.Text = row.Cells["PetName"].Value.ToString();
                cboSpecies.Text = row.Cells["Species"].Value.ToString();
                txtBreed.Text = row.Cells["Breed"].Value.ToString();
                txtAge.Text = row.Cells["Age"].Value.ToString();

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

                if (_petService.AddPet(id, name, species, breed, age))
                {
                    MessageBox.Show("Thêm thành công!");
                    LoadData(); // Thông tin nhảy xuống bảng ngay lập tức
                    btnReset_Click(null, null); // Làm mới ô nhập
                }
            }
            catch (Exception )
            {
                MessageBox.Show("Lỗi định dạng: Tuổi phải là số!");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (_petService.UpdatePet(txtPetID.Text, txtPetName.Text, cboSpecies.Text, txtBreed.Text, int.Parse(txtAge.Text)))
                {
                    MessageBox.Show("Cập nhật thành công!");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
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

            // Đưa ComboBox về trạng thái chưa chọn
            cboSpecies.SelectedIndex = -1;

            // Mở khóa ô txtPetID (phòng trường hợp đang bị ReadOnly sau khi nhấn vào bảng)
            txtPetID.ReadOnly = false;

            // Đưa con trỏ chuột về ô đầu tiên để sẵn sàng nhập mới
            txtPetID.Focus();

            // Tải lại bảng để đảm bảo dữ liệu hiển thị mới nhất
            LoadData();
        }
    } }



