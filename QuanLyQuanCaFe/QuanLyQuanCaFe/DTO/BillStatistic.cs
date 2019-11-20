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

        public int Id { get => id; set => id = value; }
        public DateTime? Datecheckin { get => datecheckin; set => datecheckin = value; }
        public DateTime? Datecheckout { get => datecheckout; set => datecheckout = value; }
        public int Status { get => status; set => status = value; }
        public double Sum { get => sum; set => sum = value; }

        public BillStatistic(int id, DateTime? datecheckin, DateTime? datecheckout, int status, double sum)
        {
            this.Id = id;
            this.Datecheckin = datecheckin;
            this.Datecheckout = datecheckout;
            this.Status = status;
            this.Sum = sum;
        }
        
        public BillStatistic (DataRow row)
        {
            this.Id = (int) row["id"];
            this.Datecheckin = (row["datecheckin"] == DBNull.Value ? null : (DateTime?)row["datecheckin"]);
            this.Datecheckout = (row["datecheckout"] == DBNull.Value ? null : (DateTime?) row["datecheckout"]);
            this.Status = (int) row["status"];
            this.Sum = (double) row["sum"];
        }
    }
}
