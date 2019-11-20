using QuanLyQuanCaFe.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyQuanCaFe.DTO;
namespace QuanLyQuanCaFe
{
    public partial class fLogin : Form
    {
        private static Account loggedUser = null;

        public static Account LoggedUser { get => loggedUser; set => loggedUser = value; }

        public fLogin()
        {
            InitializeComponent();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string passWord =txbPassWord.Text;

            DataRow row = null;
            if ((row = Login(userName, passWord)) != null)
            {
                LoggedUser = new Account (row);
                fTableManager f = new fTableManager();
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu!");
            }
        }
        DataRow Login(string userName, string passWord)
        {
            return AccountDAO.Instance.Login(userName, passWord);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Bạn có thật sự muốn thoát chương trình?","Thông báo",MessageBoxButtons.OKCancel)!=System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;

            }
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
