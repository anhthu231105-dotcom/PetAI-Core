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
    public partial class FrmPaymentConfirm : Form
    {
        private double _tongPhaiTra;
        public FrmPaymentConfirm()
        {
            InitializeComponent();

        }
        public FrmPaymentConfirm(double tongTien)
        {
            InitializeComponent();
            _tongPhaiTra = tongTien;

            // Gán số tiền lên cái label3 mà Thư đã vẽ
            label3.Text = _tongPhaiTra.ToString("N0") + " VNĐ";
        }

        private void radTransfer_CheckedChanged(object sender, EventArgs e)
        {
            pnlTransfer.Visible = radTransfer.Checked;
            if (radTransfer.Checked)
            {
                // Tạo mã QR (Thư thay STK và Ngân hàng của Thư vào đây)
                string qrData = $"https://api.vietqr.io/image/970422-STK_CUA_THU-YL97X9S.jpg?amount={_tongPhaiTra}&addInfo=ThanhToanPetAI";
                picQR.Load(qrData);
            }
        }

        private void txtCustomerMoney_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtCustomerMoney.Text, out double khachDua))
            {
                double tienThoi = khachDua - _tongPhaiTra;
                lblChange.Text = tienThoi >= 0 ? tienThoi.ToString("N0") + " VNĐ" : "Chưa đủ tiền";
            }
        }

        private void FrmPaymentConfirm_Load(object sender, EventArgs e)
        {

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            // Khi bấm xác nhận, trả kết quả OK về cho Form chính
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void radCash_CheckedChanged(object sender, EventArgs e)
        {
            pnlCash.Visible = radCash.Checked;
            pnlTransfer.Visible = !radCash.Checked; // Nếu chọn Tiền mặt thì ẩn Chuyển khoản
        }
    }
}
