using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCaFe.DTO
{
    public class AccountType
    {
        private int id;
        private String caption;

        public AccountType(int id, string caption)
        {
            this.Id = id;
            this.Caption = caption;
        }

        public int Id { get => id; set => id = value; }
        public string Caption { get => caption; set => caption = value; }
    }
}
