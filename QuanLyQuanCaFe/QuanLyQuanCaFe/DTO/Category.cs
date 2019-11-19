using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace QuanLyQuanCaFe.DTO
{
    public class Category
    {
        private int id;
        private String categoryName;

        public int Id { get => id; set => id = value; }
        public string CategoryName { get => categoryName; set => categoryName = value; }

        public Category(int id, string categoryName)
        {            
            this.CategoryName = categoryName;            
            this.CategoryName = categoryName;
        }
    
        public Category (DataRow row)
        {
            this.Id = (int)row["id"];
            this.CategoryName = (String) row["name"];
        }
    }
}
