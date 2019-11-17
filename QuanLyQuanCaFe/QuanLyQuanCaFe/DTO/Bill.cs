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
        private DateTime? dateCheckIn;
        private DateTime? dateCheckOut;
        private int status;

        public Bill(int id, DateTime? dateCheckIn, DateTime? dateCheckOut, int status)
        {
            this.Id = id;
            this.dateCheckIn = dateCheckIn;
            this.dateCheckOut = dateCheckOut;
            this.status = status;
        }

        public Bill(DataRow row)
        {
            Id = (int)row["id"];
            DateCheckIn = (row["DateCheckIn"] == DBNull.Value ? null : (DateTime?)row["DateCheckIn"]);
            DateCheckIn = (row["DateCheckOut"] == DBNull.Value ? null : (DateTime?)row["DateCheckOut"]);            
            Status = (int)row["status"];
        }

        public DateTime? DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public DateTime? DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
        public int Status { get => status; set => status = value; }
        public int Id { get => id; set => id = value; }
    }
}
