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
        public fAdmin()
        {
            InitializeComponent();
           
        }

        private void btnViewBill_Click(object sender, EventArgs e)
        {
            var bills = BillDAO.Instance.GetAllBillRegardsPeriodOfTime(dtpkFromDate.Value, dtpkToDate.Value);            
            dtgvBill.DataSource = bills;
            for (int i = 0; i<dtgvBill.ColumnCount; ++i)
            {
                dtgvBill.Columns[i].HeaderText = BillStatisticHeader[i];
            }

        }

        private void BtnShowFood_Click(object sender, EventArgs e)
        {
            this.foodTableAdapter.Fill(this.quanlycafeDataSet.Food);
            dtgvTable.DataSource = this.quanlycafeDataSet.Food;
        }

        private void BtnSearchFood_Click(object sender, EventArgs e)
        {            
            List<Food> foods = FoodDAO.Instance.SelectAlike(txbSearchFoodName.Text);// Chưa bắt conjunction
            dtgvFood.DataSource = foods;
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
        }

        private void FAdmin_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'quanlycafeDataSet4.Account' table. You can move, or remove it, as needed.
            this.accountTableAdapter.Fill(this.quanlycafeDataSet4.Account);
            // TODO: This line of code loads data into the 'quanlycafeDataSet3.TableFood' table. You can move, or remove it, as needed.
            this.tableFoodTableAdapter.Fill(this.quanlycafeDataSet3.TableFood);
            // TODO: This line of code loads data into the 'quanlycafeDataSet2.FoodCategory' table. You can move, or remove it, as needed.
            this.foodCategoryTableAdapter1.Fill(this.quanlycafeDataSet2.FoodCategory);
            // TODO: This line of code loads data into the 'quanlycafeDataSet1.FoodCategory' table. You can move, or remove it, as needed.
            this.foodCategoryTableAdapter.Fill(this.quanlycafeDataSet1.FoodCategory);
            // TODO: This line of code loads data into the 'quanlycafeDataSet.Food' table. You can move, or remove it, as needed.
            this.foodTableAdapter.Fill(this.quanlycafeDataSet.Food);
        }

        private void CbFoodCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dtgvFood_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
