using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCaFe.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;
        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }
        private AccountDAO() { }
        public DataRow Login(string userName, string passWord)
        {
            string query = "USP_Login @userName , @passWord ";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[]{userName, passWord});
            if (result.Rows.Count == 0)
                return null;
            return result.Rows[0];
        }

        public bool ChangeInfo (string userName, string passWord, string newPassWord, string displayName)
        {
            if (Login (userName, passWord) == null)
                return false;
            string query = "USP_ChangeInfo @account ,  @oldpassword , @password , @displayname ";
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { userName, passWord, newPassWord,  displayName});
            return (result > 0);
        }
    }
}
