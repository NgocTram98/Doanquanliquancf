using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace QuanLyQuanCaFe.DTO
{
    public class Menu
    {
        String name;
        int count;
        float price;
        float money;

        public string Name { get => name; set => name = value; }
        public int Count { get => count; set => count = value; }
        public float Price { get => price; set => price = value; }
        public float Money { get => money; set => money = value; }

        public Menu(string name, int count, float price, int money)
        {
            this.Name = name;
            this.Count = count;
            this.Price = price;
            this.Money = money;
        }

        public Menu (DataRow row)
        {
            this.Name = (String)row["name"];
            this.Count= (int)row["count"];
            this.Price = (float)row["price"];
            this.Money = (float)row["sum"];
        }
    }
}
