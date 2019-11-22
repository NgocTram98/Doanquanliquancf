using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanCaFe.DTO
{
    public class TableStatus
    {
        String id;
        String statusName;

        public TableStatus(string id, string statusName)
        {
            this.Id = id;
            this.StatusName = statusName;
        }

        public string Id { get => id; set => id = value; }
        public string StatusName { get => statusName; set => statusName = value; }
    }
}
