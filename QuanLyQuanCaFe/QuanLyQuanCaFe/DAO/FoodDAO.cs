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

        public bool InsertFood (String name, int category, double price)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("USP_InsertFood @name , @category , @price ", new object[] { name, category, price });
            return result > 0;
        }

        public bool DeleteFood (int id) 
        {
            int result = DataProvider.Instance.ExecuteNonQuery("DELETE FROM dbo.Food WHERE id=" + id);
            return result > 0;

        }
        public bool EditFood (int id, String name, double price, int idCategory)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("USP_EditFood @id , @name , @category , @price ", new object[] { id, name, idCategory, price });
            return result > 0;
        }

        public List<Food> SelectAlike(String pattern)
        {
            List<Food> foodlist = new List<Food>();
            DataTable table = DataProvider.Instance.ExecuteQuery("USP_FindFood @name ", new object[] {pattern});// Sửa bug chỗ này, có N'@Str' mới ổn
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