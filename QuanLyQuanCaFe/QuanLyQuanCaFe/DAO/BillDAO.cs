using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public int GetUncheckedBillIDByTableID (int idTable)
        {
            return (int) DataProvider.Instance.ExecuteScalar("select id from bill where idtable = " + idTable + " and status = 0");
        }

    }
}
