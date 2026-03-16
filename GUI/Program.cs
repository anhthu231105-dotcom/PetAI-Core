using GUI.Management;
using GUI.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //CÁCH 1: Chạy thẳng vào Form chính để test giao diện nhanh
          //  Application.Run(new FrmMain());

             // CÁCH 2: Quy trình chuẩn (Chạy Login trước)
           FrmLogin login = new FrmLogin();
            if (login.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new FrmMain());
            }
            
        }
    }
}
