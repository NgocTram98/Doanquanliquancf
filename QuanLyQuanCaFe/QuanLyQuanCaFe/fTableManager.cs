using QuanLyQuanCaFe.DAO;
using QuanLyQuanCaFe.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCaFe
{
    public partial class fTableManager : Form
    {
        public fTableManager()
        {
            InitializeComponent();
            LoadTable();
        }
        #region Method
        void LoadTable()
        {
            List<Table> tableList = TableDAO.Instance.LoadTableList();
            foreach (Table item in tableList)
            {
                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeight };
                btn.Text = item.Name + Environment.NewLine + item.Status;

                btn.Click += btn_Click;
                btn.Tag = item;

                flpTable.Controls.Add(btn);
                switch (item.Status)
                {
                    case "trong":
                        btn.BackColor = Color.Aqua;
                        break;
                    default:
                        btn.BackColor = Color.LightPink;
                        break;
                }
            }
        }

        void ShowBill(int tableId)
        {
            //MessageBox.Show(tableId + "");
            lsvBill.Items.Clear();
            var list = MenuDAO.Instance.GetListMenuByTable(tableId);

            double sum = 0;
            foreach (var info in list)
            {
                sum += info.Money;
                var lsvItem = new ListViewItem(info.Name);
                lsvItem.SubItems.Add(info.Count+"");
                lsvItem.SubItems.Add(info.Price + "");
                lsvItem.SubItems.Add(info.Money + "");
                lsvBill.Items.Add(lsvItem);
            }
            CultureInfo culture = new CultureInfo("vi");
            lbTongTien.Text = sum.ToString("c", culture);
        }

        #endregion

        #region Events

        private void btn_Click (object sender, EventArgs e)
        {
            int tableId = ((sender as Button).Tag as Table).ID;
            ShowBill(tableId);
        }

        private void fTableManager_Load(object sender, EventArgs e)
        {

        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfile f = new fAccountProfile();
            f.ShowDialog();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.ShowDialog();
        }
        #endregion

        private void MenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
