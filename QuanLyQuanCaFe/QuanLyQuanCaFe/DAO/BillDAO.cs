using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using QuanLyQuanCaFe.DTO;

namespace QuanLyQuanCaFe.DAO
{
    public class BillDAO
    {
        //int id;
        //DateTime? DateCheckIn, DateCheckOut;
        //int idTable;
        //int status;

        private static BillDAO instance;          

        public static BillDAO Instance
        {
            get
            {
                if (instance == null)
                    return (instance = new BillDAO());
                return instance;
            }            
        }

        public BillDAO()
        {
            
        }

        // Thất bại trả về -1
        // Sao không raise exception nhỉ :), nhằng
        public int GetUncheckedBillIDByTableID (int idTable)
        {
            String sql = "select * from dbo.bill where idtable = " + idTable + " and status = 0";
            DataTable table = DataProvider.Instance.ExecuteQuery(sql);
            if (table.Rows.Count>0)
            {
                Bill bill = new Bill(table.Rows[0]);
                return bill.Id;
            }
            return -1;
        }

    }
}
