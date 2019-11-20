using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace QuanLyQuanCaFe.DTO
{
    public class BillStatistic
    {
        int id;
        DateTime? datecheckin;
        DateTime? datecheckout;
        int status;
        double sum;
        int discount;

        public int Id { get => id; set => id = value; }
        public DateTime? Datecheckin { get => datecheckin; set => datecheckin = value; }
        public DateTime? Datecheckout { get => datecheckout; set => datecheckout = value; }
        public String Status { get => (status == 0 ? "Chưa thanh toán" : "ĐÃ THANH TOÁN"); }
        public int Discount { get => discount; set => discount = value; }
        public double Sum { get => sum; set => sum = value; }
        

        public BillStatistic(int id, DateTime? datecheckin, DateTime? datecheckout, int status, double sum, int discount)
        {
            this.id = id;
            this.datecheckin = datecheckin;
            this.datecheckout = datecheckout;
            this.status = status;
            this.sum = sum;
            this.discount = discount;
        }

        public BillStatistic (DataRow row)
        {
            this.Id = (int) row["id"];
            this.Datecheckin = (row["datecheckin"] == DBNull.Value ? null : (DateTime?)row["datecheckin"]);
            this.Datecheckout = (row["datecheckout"] == DBNull.Value ? null : (DateTime?) row["datecheckout"]);
            this.status = (int) row["status"];
            this.Sum = (double) row["sum"];
            this.discount = (int)row["discount"];
        }
    }
}
