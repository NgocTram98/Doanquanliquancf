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
    }
}
