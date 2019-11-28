using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyQuanCaFe.DTO;
using System.Data;

namespace QuanLyQuanCaFe.DAO
{
    class BillInfoDAO
    {
        private static BillInfoDAO instance = null;

        public static BillInfoDAO Instance { get => (instance == null ? (instance = new BillInfoDAO()) : instance); set => instance = value; }
        
        public BillInfoDAO ()
        {
        }

        public List<BillInfo> GetBillInfos (int id)
        {
            List<BillInfo> infos = new List<BillInfo> ();
            DataTable table = DataProvider.Instance.ExecuteQuery("select * from dbo.billinfo where idbill=" + id);
            for (int i = 0; i<table.Rows.Count; ++i)
            {
                infos.Add(new BillInfo(table.Rows[i]));
            }
            return infos;
        }

        public void InsertBillInfos (int billId, int foodId, int count)
        {
            String sql = "SELECT * From dbo.billinfo WHERE idBill=" + billId + " AND idFood=" + foodId;
            DataTable table = DataProvider.Instance.ExecuteQuery(sql);
       
            if (table.Rows.Count > 0)
            {
                BillInfo info = new BillInfo(table.Rows[0]);
                if (info.Count + count <= 0)
                {
                    sql = "DELETE FROM dbo.billinfo WHERE idBill=" + billId + " AND idFood=" + foodId;
                    DataProvider.Instance.ExecuteScalar(sql);
                    BillDAO.Instance.CheckAndRemoveInvalidBill(billId);
                }
                else
                {
                    sql = "UPDATE dbo.billinfo SET count=count+" + count + " WHERE idBill=" + billId + " AND idFood=" + foodId;
                    DataProvider.Instance.ExecuteScalar(sql);
                }
            } else {
                sql = "INSERT INTO dbo.billinfo (idBill,idFood,count) VALUES (" + billId + ", " + foodId + ", " + count + ")";
                DataProvider.Instance.ExecuteScalar(sql);
            }         
        }
    }
}
