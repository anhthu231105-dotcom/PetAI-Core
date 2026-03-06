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
                cbCustomer.DataSource = _cusBus.GetAll(); // Hoặc .GetAll()
                cbCustomer.DisplayMember = "CustomerName";
                cbCustomer.ValueMember = "CustomerID";

                cbService.SelectedIndex = -1;
                cbCustomer.SelectedIndex = -1;
            }
            catch (Exception ) 
            {
                // Xóa bỏ catch trống để dễ tìm lỗi nếu có
                // MessageBox.Show("Lỗi load dữ liệu: " + ex.Message);
            }
        }
    }
    }
