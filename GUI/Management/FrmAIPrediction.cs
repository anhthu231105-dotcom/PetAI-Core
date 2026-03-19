using BUS.AI_Services;
using BUS.Services;
using DAL.AI_Models;
using DTO.AI_Models;
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
    public partial class FrmAIPrediction : Form
    {
        // 1. Khai báo các đối tượng cần thiết
        private readonly HealthAssistant _aiAssistant = new HealthAssistant();
        private HealthPredictionDTO currentPrediction; // Giỏ đựng kết quả dự đoán
                                                       //  private readonly HealthAssistant _aiAssistant = new HealthAssistant();
        private PetService _petService = new PetService();
        public FrmAIPrediction()
        {
            InitializeComponent();

        }

        private void FrmAIPrediction_Load(object sender, EventArgs e)
        {
            // Thiết lập giá trị mặc định cho các lựa chọn
            cboAppetite.SelectedIndex = 0; // "Bình thường"
            cboActivity.SelectedIndex = 0;  // "Linh hoạt"
                                            // 1. Lấy bảng dữ liệu thú cưng về
            DataTable dt = _petService.GetAllPets();

            // THỨ TỰ CHUẨN: Xóa cũ -> Gán Member -> Gán dữ liệu
            cboPetList.DataSource = null;
            cboPetList.DisplayMember = "PetName";
            cboPetList.ValueMember = "PetID";
            cboPetList.DataSource = dt; // Chỉ gán DataSource ở dòng cuối cùng này thôi
            // Thư nhớ kiểm tra hàm Load dữ liệu cho cboPetList ở đây nhé
        }

        // ==========================================
        // 2. NÚT DỰ ĐOÁN: Gọi AI từ Python
        // ==========================================
        private async void btnPredict_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Lấy dữ liệu (Thư viết phần này chuẩn rồi!)
                string temp = txtTemp.Text;
                string appetite = cboAppetite.Text;
                string activity = cboActivity.Text;

                // 2. Gọi tầng BUS (Hàm này phải là duy nhất trong HealthAssistant.cs)
                // Mình gán trực tiếp vào currentPrediction để dùng cho nút LƯU sau này
                currentPrediction = await _aiAssistant.GetAIHealthPrediction(temp, appetite, activity);

                if (currentPrediction != null)
                {
                    // 3. Hiển thị lên Form
                    txtConfidence.Text = currentPrediction.Confidence.ToString() + "%";
                    txtAdvice.Text = currentPrediction.Advice;
                    lblStatus.Text = currentPrediction.Result;

                    // 4. Xử lý màu sắc sinh động
                    if (currentPrediction.Result == "Cảnh báo nguy hiểm")
                    {
                        lblStatus.ForeColor = Color.Red;
                        txtAdvice.BackColor = Color.MistyRose;
                    }
                    else
                    {
                        lblStatus.ForeColor = Color.Green;
                        txtAdvice.BackColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                // Thông báo lỗi thân thiện nếu chưa bật Python
                MessageBox.Show("Thư ơi, kiểm tra lại: " + ex.Message, "Thông báo");
            }
        }

        // ==========================================
        // 3. NÚT LƯU KẾT QUẢ: Lưu vào SQL Server
        // ==========================================
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem đã bấm dự đoán chưa
            if (currentPrediction == null || currentPrediction.Result == "Lỗi kết nối")
            {
                MessageBox.Show("Thư phải bấm dự đoán thành công trước khi lưu nhé!", "Thông báo");
                return;
            }

            try
            {
                // Gán mã thú cưng từ ComboBox
                if (cboPetList.SelectedValue != null)
                {
                    currentPrediction.PetID = cboPetList.SelectedValue.ToString();
                }
                else
                {
                    // Nếu chưa load kịp ID, Thư có thể tạm gán để test
                    currentPrediction.PetID = "P001";
                }

                // Điền thêm các thông số cần thiết để khớp với bảng HealthRecord
                currentPrediction.Temperature = double.Parse(txtTemp.Text);
                currentPrediction.HeartRate = 80; // Giá trị mặc định hoặc lấy từ cảm biến
                currentPrediction.DiagnosisID = "D001"; // Mặc định mã bệnh
                currentPrediction.PredictionDate = DateTime.Now;

                // Gọi tầng DAL để thực hiện câu lệnh INSERT vào SQL
                bool isSaved = HealthPredictionDAL.Instance.SavePrediction(currentPrediction);

                if (isSaved)
                {
                    MessageBox.Show("Đã lưu lịch sử sức khỏe của bé vào Database thành công!", "Tuyệt vời Thư nhé");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message);
            }
        }

        private void cboPetList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem ComboBox có đang chọn dòng nào hợp lệ không
            if (cboPetList.SelectedItem == null) return;

            try
            {
                // 2. Ép kiểu Item đang chọn về DataRowView để truy cập các cột dữ liệu
                DataRowView row = cboPetList.SelectedItem as DataRowView;

                if (row != null)
                {
                    // 3. Lấy giá trị cột "Weight". 
                    // Thư nhớ kiểm tra xem trong SQL tên cột là "Weight" hay "PetWeight" nhé!
                    txtWeight.Text = row["Weight"].ToString();
                }
            }
            catch (Exception ex)
            {
                // Ghi log nhẹ nếu có lỗi xảy ra để Thư biết đường sửa
                Console.WriteLine("Lỗi lấy cân nặng: " + ex.Message);
            }
        }
    }
}
