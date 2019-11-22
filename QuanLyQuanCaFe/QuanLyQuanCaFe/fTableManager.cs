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
        int currentTableId = -1;
        public fTableManager()
        {
            InitializeComponent();
            thôngTinTàiKhoảnToolStripMenuItem.Text = thôngTinTàiKhoảnToolStripMenuItem.Text + "(" + fLogin.LoggedUser.DisplayName + ")";
            LoadTable();
            LoadCategoryList();
        }
        #region Method
        void LoadCategoryList()
        {
            var categories = CategoryDAO.Instance.GetCategoryList();
            cbCategory.Items.Clear();
            cbCategory.DataSource = categories;
            cbFood.ValueMember = "Id";
            cbCategory.DisplayMember = "CategoryName";            
        }

        void LoadFood (int idCategory)
        {
            var food = FoodDAO.Instance.GetFoodByCategory (idCategory);           
            cbFood.DataSource = food;
            cbFood.ValueMember = "Id";
            cbFood.DisplayMember = "Name";                  
        }

        public void LoadTable() // Vẽ lại các bàn ăn, hàm này có đpt lớn, nên cân nhắc khi gọi, có thể bị gọi từ fAdmin form
        {
            flpTable.Controls.Clear();

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


        void ShowMoney (int idTable, double discountMoney) // Đã bao gồm tính giảm giá
        {
            double sum = MenuDAO.Instance.GetTotalMoneyByTable (idTable);
            sum = Math.Max(sum - discountMoney, 0);
            CultureInfo culture = new CultureInfo("vi");
            lbTongTien.Text = sum.ToString("c", culture);
        }

        void UpdateChange (int tableId) // Cập nhật những thay đổi của bàn tableId: Bao gồm: Bill (chi tiết), Tổng tiền, Bàn trống để dời ...
        {
            ShowBill(tableId);
            ShowMoney(tableId, (double)nmDisCount.Value);        
            cbSwitchTable.ValueMember = "Id";
            cbSwitchTable.DisplayMember = "Name";
            cbSwitchTable.DataSource = TableDAO.Instance.LoadTableWithStatus ("trong");
            nmDisCount.Value = 0;
        }
        void ShowBill(int tableId)
        {
            //MessageBox.Show(tableId + "");
            
            lsvBill.Items.Clear();
            var list = MenuDAO.Instance.GetListMenuByTable(tableId);

            foreach (var info in list)
            {                
                var lsvItem = new ListViewItem(info.Name);
                lsvItem.SubItems.Add(info.Count+"");
                lsvItem.SubItems.Add(info.Price + "");
                lsvItem.SubItems.Add(info.Money + "");
                lsvBill.Items.Add(lsvItem);
            }

            currentTableId = tableId;
        }

        public void insertFoodToBill (int idTable, int idFood, int count)
        {
            if (currentTableId == -1) // chưa chọn bàn:
                return; // hãy hiện thông báo

            int billId = BillDAO.Instance.GetUncheckedBillIDByTableID(idTable);
            if (billId == -1)
            {
                billId = BillDAO.Instance.InsertBill(currentTableId);
                LoadTable(); // Chỗ này chưa tối ưu, nhưng vì mục đích tiết kiệm thời gian code, tạm thời để như vậy !
            }

            BillInfoDAO.Instance.InsertBillInfos(billId, idFood, count);

            UpdateChange(currentTableId);
        }

        #endregion

        #region Events

        private void btn_Click (object sender, EventArgs e)
        {
            int tableId = ((sender as Button).Tag as Table).ID;
            UpdateChange(tableId);
        }

        private void fTableManager_Load(object sender, EventArgs e)
        {

        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedValue = (int) (((ComboBox) sender).SelectedValue as Category).Id;
            LoadFood(selectedValue);
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
            fAdmin f = new fAdmin(this);
            f.ShowDialog();
        }
        #endregion

        private void MenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void BtnAddFood_Click(object sender, EventArgs e)
        {
            int idFood = (int) cbFood.SelectedValue;
            int idCount = (int)numericUpDown1.Value;
            int idTable = currentTableId;
            insertFoodToBill(idTable, idFood, idCount);
        }

        private void NmDisCount_ValueChanged(object sender, EventArgs e)
        {
            if (currentTableId == -1)
                return;

            double value = (double) (sender as NumericUpDown).Value;           
            ShowMoney(currentTableId, value);
        }

        private void BtnCheckOut_Click(object sender, EventArgs e)
        {
            if (currentTableId == -1)
                return;
            int billId = BillDAO.Instance.GetUncheckedBillIDByTableID(currentTableId);
            if (billId != -1)
            {
                if (MessageBox.Show("Số tiền: " + lbTongTien.Text + Environment.NewLine+" Bạn có chắc muốn thanh toán đơn?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int discount = (int) nmDisCount.Value;
                    BillDAO.Instance.checkOut(currentTableId, discount);
                    LoadTable();
                    UpdateChange(currentTableId);
                }
                
            } else
            {
                MessageBox.Show("Không thể thanh toán bàn trống");
            }
        }

        private void BtnSwitchTable_Click(object sender, EventArgs e)
        {
            if (cbSwitchTable.SelectedValue == null)
                return;

            int idTableFrom = currentTableId;
            int idTableMovedTo = (int)cbSwitchTable.SelectedValue;
            BillDAO.Instance.moveToTable(idTableFrom, idTableMovedTo);
            currentTableId = idTableMovedTo;
            LoadTable();
            UpdateChange(currentTableId);
        }
    }
}
