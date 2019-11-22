using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using QuanLyQuanCaFe.DTO;

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

        //refer from https://stackoverflow.com/questions/11454004/calculate-a-md5-hash-from-a-string by Vinh Phuc Ta Dang
        private static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public List<AccountType> GetAllAccountTypes ()
        {
            List<AccountType> result = new List<AccountType>();
            result.Add(new AccountType(0, "Chủ quán"));
            result.Add(new AccountType(1, "Nhân viên"));
            return result;  
        }

        public bool AddAccount(String userName, String displayName, String password, int type) // type = 0 là chủ, type 1: nhân viên, chỉ có chủ mới xem và điều chỉnh được tab này
        {
            var md5Password = CreateMD5(password);
            int result = 1;
            try
            {
                result = DataProvider.Instance.ExecuteNonQuery("USP_InsertAccount @username , @displayName , @passWord , @type ", new object[] { userName, displayName, md5Password, type });

            } catch (Exception e)
            {
                return false;
            }

            return result > 0;
        }

        public bool EditAccount(String userName, String displayName, String password, int type)
        {
            var md5Password = CreateMD5(password);
            int result = 1;
            try
            {
                result = DataProvider.Instance.ExecuteNonQuery("USP_EditAccount @username , @displayName , @passWord , @type ", new object[] { userName, displayName, md5Password, type });

            }
            catch (Exception e)
            {
                return false;
            }
            return result > 0;
        }

        public bool DeleteAccount(String username)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("USP_DeleteAccount @username ", new object[] { username});
            return result > 0;
        }

        public DataRow Login(string userName, string passWord)
        {

            var passwordMD5 = CreateMD5(passWord);
            string query = "USP_Login @userName , @passWord ";

            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[]{userName, passwordMD5});
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
