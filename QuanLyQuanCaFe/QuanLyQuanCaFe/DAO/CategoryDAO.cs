using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using QuanLyQuanCaFe.DTO;

namespace QuanLyQuanCaFe.DAO
{
    public class CategoryDAO
    {

        private static CategoryDAO instance;
        

        public static CategoryDAO Instance { get => (instance == null ? new CategoryDAO () : instance); set => instance = value; }

        
        public List<Category> GetCategoryList ()
        {
            List<Category> infos = new List<Category>();
            string sql = "SELECT * FROM dbo.FoodCategory";
            DataTable table = DataProvider.Instance.ExecuteQuery(sql);
            foreach (var row in table.Rows) {
                infos.Add(new Category((DataRow) row));
            }
            return infos;
        }

        public bool AddCategory(String name)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("USP_InsertCategory @name ", new object[] { name});
            return result>0;
        }

        public bool EditCategory (int id, String name)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("USP_EditCategory @id , @name ", new object[] { id, name });
            return result > 0;
        }

        public bool DeleteCategory(int id)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("DELETE FROM dbo.FoodCategory WHERE id = " + id);
            return result > 0;
        }
    }
}
