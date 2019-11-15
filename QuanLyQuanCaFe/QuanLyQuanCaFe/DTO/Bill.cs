using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace QuanLyQuanCaFe.DTO
{
    public class Bill
    {
        private int id;
        DateTime? DateCheckIn, DateCheckOut;
        private int status;

        public Bill(DataRow row)
        {
            id = (int)row["id"];
            DateCheckIn = (DateTime?) row["DateCheckIn"];
            DateCheckOut = (DateTime?)row["DateCheckOut"];
            status = (int)row["status"];
        }

    }
}
