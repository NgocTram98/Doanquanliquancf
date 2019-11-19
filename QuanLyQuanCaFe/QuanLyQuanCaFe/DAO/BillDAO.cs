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

        public int InsertBill(int idTable) // Chèn bill vào một bàn (cho bởi Id của bàn đó)
        {
            String sql = "INSERT INTO dbo.bill (datecheckout, idTable, status) VALUES (NULL, "+idTable+", 0) UPDATE dbo.TableFood SET status='co nguoi' WHERE id="+idTable;
            DataProvider.Instance.ExecuteScalar(sql);
            // Gửi lại id Bill mới
            return GetUncheckedBillIDByTableID (idTable);
        }

        private int __faceRemoveBillByTable (int idTable)
        {
            int billId = GetUncheckedBillIDByTableID(idTable);
            if (billId == -1)
                return -1;

            String sql = "UPDATE dbo.bill SET status=1 WHERE id=" + billId + " UPDATE dbo.tablefood SET status='trong' WHERE id=" + idTable;
            DataProvider.Instance.ExecuteScalar(sql);

            return billId;
        }
        public int checkOut (int idTable)
        {
            return __faceRemoveBillByTable(idTable);
        }

        public int moveToTable (int idTable, int idTableMovedTo)
        {
            int billId = __faceRemoveBillByTable(idTable); // thực ra đặt là fromBill thì đúng hơn :)
            if (billId == -1)
                return -1;
            int toBill = GetUncheckedBillIDByTableID(idTable);
            if (toBill != -1) // Ở đó đang có người ngồi 
            {
                // Gộp bill
                String sql = "UPDATE dbo.billinfo SET idBill=" + toBill + " WHERE idBill=" + billId;
                DataProvider.Instance.ExecuteScalar(sql);
            } else
            {
                // Này dễ hơn, chỉ cần sửa cái bàn là xong:)
                String sql = "UPDATE dbo.billinfo SET idTable=" + idTableMovedTo + " WHERE idBill=" + billId 
                    + " UPDATE dbo.TableFood SET status='co nguoi' WHERE id="+idTableMovedTo;
                DataProvider.Instance.ExecuteScalar(sql); 
            }
            return idTableMovedTo;
        }
    }

    
}
