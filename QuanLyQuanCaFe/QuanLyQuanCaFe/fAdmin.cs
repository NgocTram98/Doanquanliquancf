using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QuanLyQuanCaFe.DAO;
using QuanLyQuanCaFe.DTO;

namespace QuanLyQuanCaFe
{
    public partial class fAdmin : Form
    {
        String[] BillStatisticHeader = new string[] { "Id hóa đơn", "Ngày vào", "Ngày ra", "Trạng thái", "Giảm giá (%)", "Tổng tiền" };
        List<Category> categories = null;
        List<BillStatistic> bills = new List<BillStatistic>();
        public const int billPerPage = 10;

        private fTableManager caller;

        public fAdmin(fTableManager caller) // Có thể bị gọi từ form Table Manager
        {
            InitializeComponent();
            this.caller = caller;
        }
        private void FAdmin_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'quanlycafeDataSet4.Account' table. You can move, or remove it, as needed.
            this.accountTableAdapter.Fill(this.quanlycafeDataSet4.Account);
            // TODO: This line of code loads data into the 'quanlycafeDataSet3.TableFood' table. You can move, or remove it, as needed.
            this.tableFoodTableAdapter.Fill(this.quanlycafeDataSet3.TableFood);
            // TODO: This line of code loads data into the 'quanlycafeDataSet1.FoodCategory' table. You can move, or remove it, as needed.
            this.foodCategoryTableAdapter.Fill(this.quanlycafeDataSet1.FoodCategory);
            // TODO: This line of code loads data into the 'quanlycafeDataSet.Food' table. You can move, or remove it, as needed.
            this.foodTableAdapter.Fill(this.quanlycafeDataSet.Food);
        }



        #region Bill
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            bills = BillDAO.Instance.GetAllBillRegardsPeriodOfTime(dtpkFromDate.Value, dtpkToDate.Value);            
            goToPage(0); // sure that page exists
            txtPage.Text = "1";
            txtSumPage.Text = bills.Count/billPerPage+(bills.Count % billPerPage == 0 ? 0 : 1)+"";
            //MessageBox.Show(bills.Count + ""); // Debug thôi
        }

        private List<BillStatistic> getPageBills(int page, int partionSize) // page tính từ 0
        {
            List<BillStatistic> partion = new List<BillStatistic>();
            int len = bills.Count;

            if (len / partionSize >= page)
            {
                int start = page * partionSize;
                int end = Math.Min((page + 1) * partionSize, bills.Count);
                for (int i = start; i < end; ++i)
                {
                    partion.Add(bills[i]);
                }

            }
            return partion;
        }

        private void goToPage(int page)
        {
            if (page < 0 || page >= ((bills.Count/billPerPage+(bills.Count % billPerPage == 0? 0: 1))))
            {
                MessageBox.Show("Trang không tồn tại");
            } else
            {
                var partion = getPageBills(page, billPerPage);
                dtgvBill.DataSource = partion;
                for (int i = 0; i < dtgvBill.ColumnCount; ++i)
                {
                    dtgvBill.Columns[i].HeaderText = BillStatisticHeader[i];
                }

                txtPage.Text = (page + 1) + "";
            }

        }


        private void BtnGo_Click(object sender, EventArgs e)
        {
            try
            {
                int pages = int.Parse(txtPage.Text)-1;
                goToPage(pages);
            } catch(Exception)
            {
                MessageBox.Show("Không thể đi đến trang, Số trang không chứa ký tự");
            }
        }

        private void BtnFirst_Click(object sender, EventArgs e)
        {
            goToPage(0);
        }

        private void BtnLast_Click(object sender, EventArgs e)
        {
            goToPage(bills.Count/billPerPage);
        }
        #endregion

        #region Food
        private void BtnShowFood_Click(object sender, EventArgs e)
        {
            FoodDataUpdateChange();
        }

        private void BtnSearchFood_Click(object sender, EventArgs e)
        {            
            var foods = FoodDAO.Instance.SelectAlike(txbSearchFoodName.Text);// Chưa bắt conjunction
            //this.quanlycafeDataSet.Food.Rows.Clear();            
            foodBindingSource.DataSource = foods;            
        }

        private void TxbFoodName_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnAddFood_Click(object sender, EventArgs e)
        {
            String name = txtFoodName.Text;
            int categoryId = (int) cbFoodCategory.SelectedValue;
            double price = (double)nmFoodPrice.Value;
            FoodDAO.Instance.InsertFood(name, categoryId, price);
            FoodDataUpdateChange();
        }

        private void LoadCategoryIntoForm ()
        {
            categories = CategoryDAO.Instance.GetCategoryList();            
            cbFoodCategory.DataSource = categories;
            cbFoodCategory.DisplayMember = "CategoryName";
            cbFoodCategory.ValueMember = "Id";
        }        

        private void FoodDataUpdateChange () // độ phức tạp hơi to, nhưng với số lượng món không qúa 10^4, hàm này vô tư
        {
            foodTableAdapter.Fill(this.quanlycafeDataSet.Food);         
            foodBindingSource.DataSource = this.quanlycafeDataSet.Food;
            if (caller != null) // Nếu có một form Table Manager gọi tới
                caller.refresh(); // Gọi vẽ lại các bàn ăn, coi như cập nhật
        }


        

        private void dtgvFood_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnEditFood_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txbFoodID.Text);
            String name = txtFoodName.Text;
            int categoryId = (int)cbFoodCategory.SelectedValue;
            double price = (double)nmFoodPrice.Value;
            FoodDAO.Instance.EditFood(id, name, price, categoryId);
            // TODO: Chuyển về dòng đang được thực hiện
            FoodDataUpdateChange();                                              
        }

        private void BtnDeleteFood_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txbFoodID.Text);
            if (FoodDAO.Instance.DeleteFood(id) == false)
                MessageBox.Show("XÓA thất bại");
            FoodDataUpdateChange();
        }
        #endregion

        
        #region Category

        private void CategoryDataUpdateChange() // độ phức tạp hơi to, nhưng với số lượng món không qúa 10^4, hàm này vô tư
        {
            // Cập nhật lại tất cả các thay đổi 
            // Để tiện thì tạm thời Fill vào hết
            this.foodCategoryTableAdapter.Fill(this.quanlycafeDataSet1.FoodCategory);
            foodCategoryBindingSource.DataSource = this.quanlycafeDataSet1.FoodCategory;
        }

        private void CbFoodCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            String name = txtCategoryName.Text;
            //int id = int.Parse(txbCategoryID.Text);
            CategoryDAO.Instance.AddCategory(name);
            CategoryDataUpdateChange();
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txbCategoryID.Text);
            CategoryDAO.Instance.DeleteCategory(id);
            CategoryDataUpdateChange();
        }

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            String name = txtCategoryName.Text;
            int id = int.Parse(txbCategoryID.Text);
            CategoryDAO.Instance.EditCategory(id, name);
            CategoryDataUpdateChange();
        }

        private void btnShowCategory_Click(object sender, EventArgs e)
        {
            CategoryDataUpdateChange();
        }
        #endregion
        #region Table
        private void cbTableStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("INDEXED CHANGED");
        }
        private void TableDataUpdateChange() // độ phức tạp hơi to, nhưng với số lượng món không qúa 10^4, hàm này vô tư
        {
            // Cập nhật lại tất cả các thay đổi 
            // Để tiện thì tạm thời Fill vào hết
            this.tableFoodTableAdapter.Fill(this.quanlycafeDataSet3.TableFood);
            tableFoodBindingSource.DataSource = this.quanlycafeDataSet3.TableFood;

            if (caller != null) // Nếu có một form Table Manager gọi tới
                caller.refresh(); // Gọi vẽ lại các bàn ăn, coi như cập nhật
        }
        private void btnAddTable_Click(object sender, EventArgs e)
        {

            //int id = int.Parse(txtTableID.Text);
            String tableName = txbTableName.Text;
            TableDAO.Instance.AddTable(tableName);
            TableDataUpdateChange();
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtTableID.Text);
            //String tableName = txbTableName.Text;
            TableDAO.Instance.DeleteTable(id);
            TableDataUpdateChange();
        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtTableID.Text);
            String tableName = txbTableName.Text;
            TableDAO.Instance.EditTable(id, tableName);
            TableDataUpdateChange();
        }

        private void btnShowTable_Click(object sender, EventArgs e)
        {
            TableDataUpdateChange();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            String id = (String)cbFilter.SelectedValue;
            if (id.Equals(""))
            {
                TableDataUpdateChange();
            }
            else
            {
                var tableList = TableDAO.Instance.LoadTableWithStatus(id);
                tableFoodBindingSource.DataSource = tableList;
            }
        }

        private void LoadTableStatusToForm ()
        {
            
            cbFilter.ValueMember = "Id";
            cbFilter.DataSource = TableDAO.Instance.RetrieveAllTableStatus(); ;            
            cbFilter.DisplayMember = "StatusName";
            //MessageBox.Show("loadaed");
            
            cbTableStatus.DataSource = TableDAO.Instance.RetrieveAllTableStatusNoFilter(); 
            cbTableStatus.ValueMember = "Id";
            cbTableStatus.DisplayMember = "StatusName";
        }
        #endregion

        #region Accounts

        
        private void LoadAccountTypeIntoForm()
        {
            cbAccountType.DataSource = AccountDAO.Instance.GetAllAccountTypes();
            cbAccountType.DisplayMember = "Caption";
            cbAccountType.ValueMember = "Id";
        }
        private void tcAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            int openningTab = (int)(sender as TabControl).SelectedIndex;
            if (openningTab == 4 && fLogin.LoggedUser.Type != 0)
            {
                (sender as TabControl).TabPages[4].Controls.Clear();
                MessageBox.Show("Chỉ có tài khoản là Chủ quán mới xem được trang này", "Thông báo");
                return;
            }

            LoadCategoryIntoForm();
            LoadTableStatusToForm();
            LoadAccountTypeIntoForm();
        }

        private void BtnAddAccount_Click(object sender, EventArgs e)
        {

            String userName = txbUserName.Text,
                displayName = txbDisplayName.Text,
                password = txtPassword.Text;

            int accountType = (int)cbAccountType.SelectedValue;

            if (password.Equals(""))
            {
                MessageBox.Show("Mật khẩu không thể rỗng được");
                return;
            }

            if (accountType == 0)
            {
                if (MessageBox.Show("Chủ quán thực sự muốn cấp cho người này toàn quyền ?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (AccountDAO.Instance.AddAccount(userName, displayName, password, accountType))
                    {
                        MessageBox.Show("Thêm tài khoản thành công, phân quyền là chủ quán");
                    } else
                    {
                        MessageBox.Show("Thêm tài khoản THẤT BẠI, không thể phân quyền cho người này");
                    }
                }
            }
            else
            {
                if (AccountDAO.Instance.AddAccount(userName, displayName, password, accountType))
                {
                    MessageBox.Show("Thêm tài khoản thành công, phân quyền là nhân viên");
                }
                else
                {
                    MessageBox.Show("Thêm tài khoản thất bại, có thể thông tin tên đăng nhập bị trùng");
                    return;
                }
            }
            AccountDataUpdateChange();
        }

        private void BtnDeleteAccount_Click(object sender, EventArgs e)
        {
            String userName = txbUserName.Text;
            AccountDAO.Instance.DeleteAccount(userName);
            AccountDataUpdateChange();
        }

        private void BtnEditAccount_Click(object sender, EventArgs e)
        {
            String userName = txbUserName.Text,
                displayName = txbDisplayName.Text,
                password = txtPassword.Text;

            int accountType = (int)cbAccountType.SelectedValue;
            if (password.Equals(""))
            {
                MessageBox.Show("Mật khẩu không thể rỗng được");
                return;
            }
            if (accountType == 0)
            {
                if (MessageBox.Show("Chủ quán thực sự muốn cấp cho người này toàn quyền ?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.OK)
                {
                    AccountDAO.Instance.EditAccount(userName, displayName, password, accountType);
                    AccountDataUpdateChange();
                } 
            }

            else

            {
                AccountDAO.Instance.EditAccount(userName, displayName, password, accountType);
                AccountDataUpdateChange();
            }
            
        }

        private void AccountDataUpdateChange ()
        {
            this.accountTableAdapter.Fill(this.quanlycafeDataSet4.Account);
            accountBindingSource.DataSource = this.quanlycafeDataSet4.Account;
        }
        #endregion

        private void CbAccountType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DtgvTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            
        }

        private void DtgvTable_SelectionChanged(object sender, EventArgs e)
        {
                        
        }

        private void DtgvAccount_MultiSelectChanged(object sender, EventArgs e)
        {
            txtPassword.Text = "";
        }

        private void TxtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void TxtPage_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
