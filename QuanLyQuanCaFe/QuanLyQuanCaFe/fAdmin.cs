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
        String[] BillStatisticHeader = new string[] { "Id bàn", "Ngày vào", "Ngày ra", "Trạng thái", "Giảm giá (%)", "Tổng tiền"};
        List<Category> categories = null;
        public fAdmin()
        {
            InitializeComponent();
           
        }

        #region Bill
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            var bills = BillDAO.Instance.GetAllBillRegardsPeriodOfTime(dtpkFromDate.Value, dtpkToDate.Value);            
            dtgvBill.DataSource = bills;
            for (int i = 0; i<dtgvBill.ColumnCount; ++i)
            {
                dtgvBill.Columns[i].HeaderText = BillStatisticHeader[i];
            }

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
            cbFoodCategory.DisplayMember = "categoryName";
            cbFoodCategory.ValueMember = "Id";
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
            LoadCategoryIntoForm();            
        }

        private void FoodDataUpdateChange () // độ phức tạp hơi to, nhưng với số lượng món không qúa 10^4, hàm này vô tư
        {
            foodTableAdapter.Fill(this.quanlycafeDataSet.Food);         
            foodBindingSource.DataSource = this.quanlycafeDataSet.Food;
        }


        private void CbFoodCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            
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
    }
}
