using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using QuanLyQuanCaFe.DTO;

namespace QuanLyQuanCaFe.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;
        

        public static FoodDAO Instance { get => (instance == null ? new FoodDAO () : instance); set => instance = value; }

        public void InsertFood (String name, int category, double price)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_InsertFood @name , @category , @price ", new object[] { name, category, price });
        }
        
        public List<Food> SelectAlike (String pattern)
        {
            List<Food> foodlist = new List<Food>();
            DataTable table = DataProvider.Instance.ExecuteQuery("SELECT * FROM food WHERE name LIKE '%" + pattern + "%'");
            foreach (var row in table.Rows)
            {
                foodlist.Add(new Food((DataRow)row));
            }
            return foodlist;
        }
        public List<Food> GetFoodByCategory (int id)
        {
            List<Food> foodlist = new List<Food>();
            DataTable table = DataProvider.Instance.ExecuteQuery("SELECT * FROM food where idCategory=" + id);
            foreach (var row in table.Rows){
                foodlist.Add(new Food((DataRow) row));
            }
            return foodlist;
        }
    }
}