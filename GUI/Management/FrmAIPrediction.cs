using BUS.AI_Services;
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
        private readonly HealthAssistant _aiAssistant = new HealthAssistant();
        public FrmAIPrediction()
        {
            InitializeComponent();
            // Thiết lập giá trị mặc định cho các ComboBox để tránh bị trống
            cboAppetite.SelectedIndex = 0; // Thường là "Bình thường"
            cboActivity.SelectedIndex = 0;  // Thường là "Linh hoạt"
        }

        private async void btnPredict_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Kiểm tra xem Thư đã nhập dữ liệu chưa
                if (string.IsNullOrWhiteSpace(txtTemp.Text) || string.IsNullOrWhiteSpace(txtWeight.Text))
                {
                    MessageBox.Show("Anh Thư ơi, hãy nhập đầy đủ Thân nhiệt và Cân nặng của bé nhé!", "Thông báo");
                    return;
                }

                // 2. Lấy dữ liệu từ giao diện
                float temp;
                if (!float.TryParse(txtTemp.Text, out temp))
                {
                    MessageBox.Show("Thân nhiệt phải là số (Ví dụ: 38,5)", "Lỗi nhập liệu");
                    return;
                }

                string appetite = cboAppetite.Text;
                string activity = cboActivity.Text; // Lấy giá trị từ ComboBox Activity

                // Hiệu ứng chờ đợi cho chuyên nghiệp
                txtAdvice.Text = "Đang gửi dữ liệu sang AI Python, Thư đợi xíu nha...";
                lblStatus.Text = "Đang xử lý...";
                lblStatus.ForeColor = Color.Blue;

                // 3. Gọi Python thông qua tầng BUS
                // Hàm này sẽ đợi Python trả về kết quả rồi mới chạy tiếp
                var prediction = await _aiAssistant.GetAIHealthPrediction(temp, appetite, activity);

                // 4. Hiển thị kết quả lên các Control của Thư
                lblStatus.Text = prediction.Result; // Trạng thái (Bình thường / Nguy hiểm)
                txtAdvice.Text = prediction.Advice; // Lời khuyên chi tiết hiện vào ô Multiline

                // Trong hàm btnPredict_Click, sau khi có kết quả:
                lblStatus.Text = prediction.Result;
                txtAdvice.Text = prediction.Advice;

                // Đổi màu nền ô Lời khuyên cho nổi bật
                if (prediction.Result == "Cảnh báo nguy hiểm")
                {
                    txtAdvice.BackColor = Color.MistyRose; // Màu hồng nhạt cảnh báo
                    lblStatus.ForeColor = Color.Red;
                }
                else
                {
                    txtAdvice.BackColor = Color.Honeydew; // Màu xanh nhạt an toàn
                    lblStatus.ForeColor = Color.Green;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message + "\n(Thư nhớ kiểm tra xem file Python đã bật chưa nhé!)");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAdvice.Text) || txtAdvice.Text.Contains("Đang gửi"))
            {
                MessageBox.Show("Chưa có kết quả để lưu đâu Thư ơi!", "Nhắc nhở");
                return;
            }

            // Ở đây Thư có thể gọi PetService để lưu vào SQL Server
            MessageBox.Show("Đã lưu kết quả chẩn đoán của bé vào hệ thống!", "Thành công");
        
}

        private void FrmAIPrediction_Load(object sender, EventArgs e)
        {
            cboAppetite.SelectedIndex = 0;
            cboActivity.SelectedIndex = 0;
        }
    }
}
