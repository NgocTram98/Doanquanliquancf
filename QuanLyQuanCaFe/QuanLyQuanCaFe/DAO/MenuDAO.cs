using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using QuanLyQuanCaFe.DTO;

namespace QuanLyQuanCaFe.DAO
{
    public class MenuDAO
    {

        private static MenuDAO instance;
        

        public static MenuDAO Instance { get => (instance == null ? new MenuDAO () : instance); set => instance = value; }

        
        public List<Menu> GetListMenuByTable (int id) // 
        {
            List<Menu> infos = new List<Menu>();
            String sql = "select dbo.Food.name, dbo.billInfo.count, dbo.Food.price, dbo.Food.price* dbo.billInfo.count as sum from dbo.bill, dbo.billinfo, dbo.tableFood, dbo.Food where dbo.billinfo.idBill = dbo.bill.id and dbo.bill.idTable = dbo.tableFood.id"
            + " and dbo.billinfo.idFood = dbo.Food.id "
            + " and dbo.bill.status= 0 "
            + " and dbo.tableFood.id="+id;
            DataTable table = DataProvider.Instance.ExecuteQuery(sql);
            foreach (var row in table.Rows)
            {
                infos.Add(new Menu((DataRow) row));
            }
            return infos;
        }
    }
}
