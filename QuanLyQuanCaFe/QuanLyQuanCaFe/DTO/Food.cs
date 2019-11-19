using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace QuanLyQuanCaFe.DTO
{
    public class Food
    {
        private int id;
        private String name;
        private double price;
        private int idCategory;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public double Price { get => price; set => price = value; }
        public int IdCategory { get => idCategory; set => idCategory = value; }

        public Food(int id, string name, double price, int idCategory)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.IdCategory = idCategory;
        }
        public Food (DataRow row)
        {
            this.Id = (int) row["id"];
            this.Name = (String)row["name"];
            this.Price = (double)row["price"];
            this.IdCategory = (int)row["idCategory"];
        }
    }
}
