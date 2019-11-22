using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyQuanCaFe.DAO;

namespace QuanLyQuanCaFe
{
    public partial class fAccountProfile : Form
    {
        public fAccountProfile()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TxbDisPlayName_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            String displayName = txbDisPlayName.Text;
            String password = txbPassWord.Text;
            String newPassword = txbNewPass.Text;
            String rePassword = txbReEnterPass.Text;
            if (!newPassword.Equals( rePassword))
            {
                MessageBox.Show("Password nhập lại không khớp");
                return;
            }

            if (AccountDAO.Instance.ChangeInfo(fLogin.LoggedUser.UserName, password, newPassword, displayName))
            {
                MessageBox.Show(this, "Đã cập nhật thành công");
                Close();
            } else
            {
                MessageBox.Show(this, "Đã cập nhật THẤT BẠI. Có thể một số thông tin đã sai sót");
                
            }                

        }

        private void FAccountProfile_Load(object sender, EventArgs e)
        {
            txbUserName.Text = fLogin.LoggedUser.UserName;
            txbDisPlayName.Text = fLogin.LoggedUser.DisplayName;
        }
    }
}
