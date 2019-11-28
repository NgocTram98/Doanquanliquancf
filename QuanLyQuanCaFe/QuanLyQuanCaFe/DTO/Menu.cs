﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace QuanLyQuanCaFe.DTO
{
    public class Menu
    {
        int id;
        String name;
        int count;
        double price;
        double money;

        public string Name { get => name; set => name = value; }
        public int Count { get => count; set => count = value; }
        public double Price { get => price; set => price = value; }
        public double Money { get => money; set => money = value; }
        public int Id { get => id; set => id = value; }

        public Menu(int id, string name, int count, double price, double money)
        {
            this.Id = id;
            this.Name = name;
            this.Count = count;
            this.Price = price;
            this.Money = money;
        }

        public Menu (DataRow row)
        {
            this.Id = (int)row["id"];
            this.Name = (String)row["name"];
            this.Count= (int)row["count"];
            this.Price = (double)row["price"];
            this.Money = (double)row["sum"];
        }
    }
}
