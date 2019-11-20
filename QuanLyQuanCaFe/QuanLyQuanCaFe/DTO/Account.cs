using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace QuanLyQuanCaFe.DTO
{
    public class Account
    {
        String userName;
        String displayName;
        int type;

        public Account(string userName, string displayName, int type)
        {
            this.DisplayName = displayName;
            this.Type = type;
        }

        public Account(DataRow row)
        {
            this.UserName = (String)row["UserName"];
            this.DisplayName = (String)row["DisplayName"];
            this.Type = (int) row["Type"];
        }

        public string DisplayName { get => displayName; set => displayName = value; }
        public int Type { get => type; set => type = value; }
        public string UserName { get => userName; set => userName = value; }
    }
}
