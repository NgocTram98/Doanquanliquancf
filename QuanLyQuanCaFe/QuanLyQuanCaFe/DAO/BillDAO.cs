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
        public int GetUncheckedBillIDByTableID(int idTable)
        {
            String sql = "select * from dbo.bill where idtable = " + idTable + " and status = 0";
            DataTable table = DataProvider.Instance.ExecuteQuery(sql);
            if (table.Rows.Count > 0)
            {
                Bill bill = new Bill(table.Rows[0]);
                return bill.Id;
            }
            return -1;
        }

        public List<BillStatistic> GetAllBillRegardsPeriodOfTime(DateTime? DateCheckIn, DateTime? DateCheckOut)
        {
            String sql = "SELECT bill.id, bill.datecheckin, bill.datecheckout, bill.status, bill.discount, SUM (billinfo.count*food.price)*(100-bill.discount)/100 AS sum "
                + " FROM dbo.bill, dbo.billinfo, dbo.Food "
                +" WHERE dbo.billinfo.idBill = dbo.bill.id "
                +" AND dbo.billinfo.idFood = dbo.food.id "
                + "GROUP BY dbo.bill.id, bill.datecheckin, bill.datecheckout, bill.status,bill.discount "
                + "HAVING bill.datecheckin >= '"+DateCheckIn.Value.ToShortDateString() + "' AND(bill.datecheckout <= '" + DateCheckOut.Value.ToShortDateString() 
                + "' OR ISNULL(bill.datecheckout, '') = '') ORDER BY sum DESC";
            Console.WriteLine(sql);
            DataTable table = DataProvider.Instance.ExecuteQuery(sql);
            List<BillStatistic> result = new List<BillStatistic> ();
            foreach (var row in table.Rows)
                result.Add(new BillStatistic ((DataRow)row));
            return result;
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
        public int checkOut (int idTable, int discount)
        {
            int billId = GetUncheckedBillIDByTableID(idTable);
            if (billId == -1)
                return -1;

            String sql = "UPDATE dbo.bill SET discount=" + discount+ ", DateCheckOut=GetDate() WHERE id=" + billId; 
            // Chèn ngày cho việc Thanh Toán Bill
            DataProvider.Instance.ExecuteScalar(sql);
            // Lặp lại chỗ này cũng ko hay lắm, nhưng vì tốc độ viết chương trình :)
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
                String sql = "UPDATE dbo.bill SET status=0, idTable=" + idTableMovedTo + " WHERE id=" + billId 
                    + " UPDATE dbo.TableFood SET status='co nguoi' WHERE id="+idTableMovedTo;
                DataProvider.Instance.ExecuteScalar(sql); 
            }
            return idTableMovedTo;
        }

        public void CheckAndRemoveInvalidBill (int billId)
        {
            String sql = "SELECT * FROM dbo.BillInfo WHERE idBill=" + billId;
            DataTable table = DataProvider.Instance.ExecuteQuery(sql);

            if (table.Rows.Count == 0)
            {
                sql = "UPDATE dbo.TableFood SET status='trong' WHERE id = (SELECT idTable from dbo.Bill WHERE dbo.Bill.Id="+billId+") "
                      + " ; DELETE FROM dbo.bill WHERE id=" + billId; // Này là sửa chắp vá
                DataProvider.Instance.ExecuteNonQuery(sql);
            }
        }
    }

    
}
