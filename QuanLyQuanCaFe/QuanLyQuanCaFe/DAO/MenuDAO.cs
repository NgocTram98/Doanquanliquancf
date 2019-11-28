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
            String sql = "select dbo.Food.id, dbo.Food.name, dbo.billInfo.count, dbo.Food.price, dbo.Food.price* dbo.billInfo.count as sum from dbo.bill, dbo.billinfo, dbo.tableFood, dbo.Food where dbo.billinfo.idBill = dbo.bill.id and dbo.bill.idTable = dbo.tableFood.id"
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

        public double GetTotalMoneyByTable (int id)
        {

            if (BillDAO.Instance.GetUncheckedBillIDByTableID(id) == -1)
                return 0;
            String sql = "select SUM (dbo.Food.price* dbo.billInfo.count) as sum "
            + "from dbo.bill, dbo.billinfo, dbo.tableFood, dbo.Food  where dbo.billinfo.idBill = dbo.bill.id and dbo.bill.idTable = dbo.tableFood.id"
            + " and dbo.billinfo.idFood = dbo.Food.id "
            + " and dbo.bill.status= 0 "
            + " and dbo.tableFood.id=" + id;

            Console.Write(sql);
            DataTable table = DataProvider.Instance.ExecuteQuery(sql);
            if (table.Rows[0]["sum"] == DBNull.Value)
                return 0;
            return (double)table.Rows[0]["sum"];
        }
    }
}
