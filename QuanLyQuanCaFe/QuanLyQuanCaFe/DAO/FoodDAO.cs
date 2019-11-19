﻿using System;
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