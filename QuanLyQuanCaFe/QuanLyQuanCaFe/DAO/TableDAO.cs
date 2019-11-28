using QuanLyQuanCaFe.DTO;
using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCaFe.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;

        public static TableDAO Instance
        {
            get { if(instance == null) instance =new TableDAO(); return TableDAO.instance; }
            private set { TableDAO.instance = value; }
        }
        public static int TableWidth = 90;
        public static int TableHeight = 90;
        private TableDAO() { }
        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();
            DataTable data = DataProvider.Instance.ExecuteQuery("dbo.USP_GetTableList");
            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tableList.Add(table);
            }
            return tableList;
        }

        public List<Table> LoadTableWithStatus(String status)
        {
            List<Table> tableList = new List<Table>();
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM dbo.tableFood WHERE status='"+status+"'");
            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tableList.Add(table);
            }
            return tableList;
        }

        public bool AddTable(String name)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("USP_InsertTable @name ", new object[] { name });
            return result > 0;
        }

        public bool EditTable(int id, String name)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("USP_EditTable @id , @name ", new object[] { id, name });
            return result > 0;
        }

        public bool DeleteTable(int id)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("DELETE FROM dbo.TableFood WHERE id = " + id);
            return result > 0;
        }

        public List<TableStatus> RetrieveAllTableStatus ()
        {
            List<TableStatus> collection = new List<TableStatus>();
            collection.Add(new TableStatus("", "Tất cả"));
            collection.Add(new TableStatus("trong", "Trống"));
            collection.Add(new TableStatus("co nguoi", "Có người"));
            return collection;
        }
        public List<TableStatus> RetrieveAllTableStatusNoFilter() // lấy danh sách các trạng thái, nhưng không tồn tại trạng thái trống, trạng thái do mình quy định
        {
            List<TableStatus> collection = new List<TableStatus>();            
            collection.Add(new TableStatus("trong", "Trống"));
            collection.Add(new TableStatus("co nguoi", "Có người"));
            return collection;
        }
    }
}
